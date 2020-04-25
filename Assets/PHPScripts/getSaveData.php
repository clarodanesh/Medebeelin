<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

//the select query to get saved data
$s = $dbconn->query("SELECT * FROM UnityProgress WHERE username='$data->uname'", PDO::FETCH_ASSOC);

//array to push data to
$info = array();

if($s->rowCount() > 0){
    while($row = $s->fetch()){
        if($data->uname == $row['username']){
            array_push($info, $row);
        }else{
            echo "no";
        }
    }

    //encode the response then echo it 
    //adding extra formatting for response
    $formattedjson = json_encode($info);
    $formattedjson = "{\"progressData\":".$formattedjson."}";
    echo $formattedjson;
}else{
    echo "no";
}
?>
