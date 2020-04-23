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

//insert hs into hs table
$sql = "INSERT INTO UnityHighScores (username, score) VALUES ('$data->uname', '$data->score')";

$dbconn->exec($sql);
echo "done";
?>
