<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$s = $dbconn->query("SELECT * FROM UnityProgress WHERE username='$data->uname'", PDO::FETCH_ASSOC);

$info = array();

if($s->rowCount() > 0){
    while($row = $s->fetch()){
        if($data->uname == $row['username']){
            array_push($info, $row);
        }else{
            echo "no";
        }
        //echo $row['id'];
    }

    $formattedjson = json_encode($info);
    $formattedjson = "{\"progressData\":".$formattedjson."}";
    echo $formattedjson;
}else{
    echo "no";
}
?>
