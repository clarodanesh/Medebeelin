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

//UPDATE MyGuests SET lastname='Doe' WHERE id=2
if($s->rowCount() > 0){
    while($row = $s->fetch()){
        if($data->uname == $row['username']){
            $sql = "DELETE FROM UnityProgress WHERE username='$data->uname'";
            $dbconn->exec($sql);

            $sql = "INSERT INTO UnityProgress (skin, upgrade, level, score, health, username, speed, nectarpoints, bosshealth) VALUES ('$data->skin', '$data->upgrade', '$data->level', '$data->score', '$data->health', '$data->uname', '$data->speed', '$data->nectarpoints', '$data->bosshealth')";

            $dbconn->exec($sql);
        }
        //echo $row['id'];
    }
}else if($s->rowCount() == 0){
    $sql = "INSERT INTO UnityProgress (skin, upgrade, level, score, health, username, speed, nectarpoints, bosshealth) VALUES ('$data->skin', '$data->upgrade', '$data->level', '$data->score', '$data->health', '$data->uname', '$data->speed', '$data->nectarpoints', '$data->bosshealth')";

    $dbconn->exec($sql);
    echo "done";
}
?>
