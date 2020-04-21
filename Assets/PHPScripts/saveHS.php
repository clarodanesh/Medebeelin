<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);


$sql = "INSERT INTO UnityHighScores (username, score) VALUES ('$data->uname', '$data->score')";

$dbconn->exec($sql);
echo "done";
?>
