<?php
$servername = "localhost";
$username = "diqbal";
$password = "QhlX6501";
$dbname = "diqbal";

$json = file_get_contents("php://input");

//decode the json data
$data = json_decode($json);

$dbconn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$s = $dbconn->query("SELECT * FROM UnityProgress WHERE username='$data->uname'", PDO::FETCH_ASSOC);

if($s->rowCount() > 0){
    //if saved data exists then save again
    while($row = $s->fetch()){
        if($data->uname == $row['username']){
            $sql = "DELETE FROM UnityProgress WHERE username='$data->uname'";
            $dbconn->exec($sql);

            $sql = "INSERT INTO UnityProgress (skin, upgrade, level, score, health, username, speed, nectarpoints, bosshealth) VALUES ('$data->skin', '$data->upgrade', '$data->level', '$data->score', '$data->health', '$data->uname', '$data->speed', '$data->nectarpoints', '$data->bosshealth')";

            $dbconn->exec($sql);
        }
    }
}else if($s->rowCount() == 0){
    //create a new save in the db
    $sql = "INSERT INTO UnityProgress (skin, upgrade, level, score, health, username, speed, nectarpoints, bosshealth) VALUES ('$data->skin', '$data->upgrade', '$data->level', '$data->score', '$data->health', '$data->uname', '$data->speed', '$data->nectarpoints', '$data->bosshealth')";

    $dbconn->exec($sql);
    echo "done";
}
?>
