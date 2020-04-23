<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

//decode data sent here
$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

//sql query to delete data from db
$sql = "DELETE FROM UnityProgress WHERE username='$data->uname'";

//using exec as no results returned
$dbconn->exec($sql);
echo "Record deleted successfully";
?>
