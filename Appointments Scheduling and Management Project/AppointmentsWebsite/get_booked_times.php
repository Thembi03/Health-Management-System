<?php
// Retrieve the selected date from the URL query parameter
$date = $_GET['date'];

// Your database connection code goes here
$serverName = "SALVATOREM";
$database = "Health_Management_System";

// Establish connection
$connectionInfo = array(
    "Database" => $database
);

$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    die("Connection failed: " . print_r(sqlsrv_errors(), true));
}

// Prepare SQL query to retrieve booked appointment times for the selected date
$sql = "SELECT appointment_datetime FROM spnmt WHERE CONVERT(date, appointment_datetime) = CONVERT(date, ?)";
$params = array($date);


// Execute SQL query
$stmt = sqlsrv_query($conn, $sql, $params);
if ($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}

// Array to store booked appointment times
$bookedTimes = array();

// Fetch booked appointment times and add them to the array
while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
    // Format the datetime field to the desired format
    $formattedDatetime = $row['appointment_datetime']->format('Y-m-d H:i:s');
    $bookedTimes[] = $formattedDatetime;
}

// Send booked appointment times as JSON response
header('Content-Type: application/json');
echo json_encode($bookedTimes);

// Close the database connection
sqlsrv_close($conn);
?>
