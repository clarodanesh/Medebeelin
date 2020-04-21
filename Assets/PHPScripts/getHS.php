<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

//$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$s = $dbconn->query("SELECT * FROM UnityHighScores ORDER BY score DESC", PDO::FETCH_ASSOC);

$info = array();

while($row = $s->fetch())
{
    array_push($info,$row);
}

$formattedjson = json_encode($info);
$formattedjson = "{\"hsData\":".$formattedjson."}";

echo $formattedjson;
?>
