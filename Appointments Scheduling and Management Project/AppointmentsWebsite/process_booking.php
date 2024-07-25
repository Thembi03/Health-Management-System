<?php

use PHPMailer\PHPMailer\Exception;
use PHPMailer\PHPMailer\PHPMailer;
require 'C:\xampp\htdocs\PHPMailer-master\PHPMailer-master\src\SMTP.php';
require'C:\xampp\htdocs\PHPMailer-master\PHPMailer-master\src\PHPMailer.php';
require'C:\xampp\htdocs\PHPMailer-master\PHPMailer-master\src\Exception.php';

$serverName = "SALVATOREM";
$database = "Health_Management_System";
$uid = ""; // Fill in your username
$pass = ""; // Fill in your password

// Establish connection
$connectionInfo = array(
    "Database" => $database,
    "UID" => $uid,
    "PWD" => $pass
);

$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    die("Connection failed: " . print_r(sqlsrv_errors(), true));
} else {
    echo '';
}

function sanitize_input($data)
{
    $data = trim($data);
    $data = stripslashes($data);
    $data = htmlspecialchars($data);
    return $data;
}

// Check if the form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Validate and sanitize form data
    $fullname = sanitize_input($_POST['fullname']);
    $national_id = sanitize_input($_POST['national_id']);
    $email = sanitize_input($_POST['email']);
    $phone = sanitize_input($_POST['phone']);
    $department = sanitize_input($_POST['department']);
    $message = sanitize_input($_POST['message']);
    $appointment_date = sanitize_input($_POST['appointment_date']);
    $appointment_time = sanitize_input($_POST['appointment_time']);

    // Validate email address
    if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $errors[] = "Invalid email address";
    }

    // Validate phone number
    function validate_phone($phone)
    {
        // Remove non-numeric characters from phone number
        $phone = preg_replace('/\D/', '', $phone);
        // Check if phone number contains exactly 10 digits (assuming 10-digit format)
        return (strlen($phone) === 10 && is_numeric($phone));
    }

    // Validate national ID
    function validate_national_id($national_id)
    {
        // Regular expression pattern to match the Zimbabwean national ID format
        $pattern = '/^\d{2}-\d{7}[A-Z]\d{2}$/';

        // Check if the national ID matches the pattern
        if (preg_match($pattern, $national_id)) {
            return true; // National ID is valid
        } else {
            return false; // National ID is not valid
        }
    }

    // Check if there are any errors
    if (empty($errors)) {
        // No validation errors, proceed to save the booking data in the database
        // Combine appointment date and time
        $appointment_datetime = $appointment_date . ' ' . $appointment_time;

        // Prepare SQL query to check for existing appointments on the selected date and time
        $sql_check = "SELECT * FROM spnmt WHERE appointment_datetime = ?";
        $params_check = array($appointment_datetime);

        // Execute SQL query
        $stmt_check = sqlsrv_query($conn, $sql_check, $params_check);
        if ($stmt_check === false) {
            die("Error executing query: " . print_r(sqlsrv_errors(), true));
        }

        // Check if there are any existing appointments at the selected date and time
        if (sqlsrv_has_rows($stmt_check)) {
            echo "Sorry, the selected appointment time is already booked. Please choose a different time.";
        } else {
            // No existing appointments, proceed to insert the new appointment
            // Prepare SQL query to insert data into the "appointments" table
            $sql_insert = "INSERT INTO spnmt (fullname, national_id, email, phone_number, department, Pmessage, appointment_datetime) 
                            VALUES (?, ?, ?, ?, ?, ?, ?)";
            $params_insert = array($fullname, $national_id, $email, $phone, $department, $message, $appointment_datetime);

            // Execute SQL query
            $stmt_insert = sqlsrv_query($conn, $sql_insert, $params_insert);
            if ($stmt_insert === false) {
                die("Error inserting data: " . print_r(sqlsrv_errors(), true));
            } else {
                // Show a pop-up message with the appointment details
                echo "<script>alert('Booking successful! Your appointment is on $appointment_date at $appointment_time.');</script>";

                // Send email confirmation to the user
                $mail = new PHPMailer(true); // Passing `true` enables exceptions
                try {
                    // Server settings
                    $mail->isSMTP(); // Set mailer to use SMTP
                    $mail->Host = 'smtp.gmail.com'; // Specify main and backup SMTP servers
                    $mail->SMTPAuth = true; // Enable SMTP authentication
                    $mail->Username = 'salvatoremugabe@gmail.com'; // SMTP username
                    $mail->Password = 'jbdpkfsdefzrpvdo'; // SMTP password
                    $mail->SMTPSecure = 'tls'; // Enable TLS encryption, `ssl` also accepted
                    $mail->Port = 587; // TCP port to connect to

                    // Recipients
                    $mail->setFrom('salvatoremugabe@gmail.com', 'Selected-Clinic');
                    $mail->addAddress($email, $fullname); // Add a recipient

                    // Content
                    $mail->isHTML(false); // Set email format to HTML
                    $mail->Subject = 'Appointment Confirmation';
                    $mail->Body = "Dear $fullname,\n\nYour appointment has been confirmed.\nDate: $appointment_date\nTime: $appointment_time\n\nThank you for choosing Selected-Clinic.";

                    $mail->send();
                    echo 'Appointment confirmation email sent to ' . $email;
                } catch (Exception $e) {
                    echo 'Failed to send appointment confirmation email. Error: ' . $mail->ErrorInfo;
                }
            }
        }
    } else {
        // Validation errors occurred, send response with errors
        http_response_code(400);
        echo json_encode($errors);
    }
} else {
    // Handle invalid request method
    http_response_code(400);
    echo "Invalid request";
}
?>
