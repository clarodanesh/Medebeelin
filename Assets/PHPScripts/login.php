<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$s = $dbconn->query("SELECT id, password FROM UnityLogin WHERE username='$data->uname'", PDO::FETCH_ASSOC);

//if a user exists with this username then either show user exists or do an fb login
if($s->rowCount() > 0){
    while($row = $s->fetch()){
        if($data->pass == "facebookLogin"){
            "done";
        }
        else if($data->pass == $row['password']){
            echo "done";
        }else{
            echo "user already exists";
        }
        //echo $row['id'];
    }
}else if($s->rowCount() == 0){
    //facebook logins have preset passwords
    if($data->pass == "facebookLogin"){
        $sql = "INSERT INTO UnityLogin (username) VALUES ('$data->uname')";
    }else{
        //insert the data into db
        $sql = "INSERT INTO UnityLogin (username, password) VALUES ('$data->uname', '$data->pass')";
    }

    $dbconn->exec($sql);
    echo "done";
}
?>
