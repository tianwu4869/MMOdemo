<?php
$servername = "localhost";
$username = "root";
$password = "a2001360095";
$db = "mmo_shooting";
$name = $_POST['name'];

try{
    // Create connection
    $conn = new PDO("mysql:host=$servername;dbname=$db", $username, $password);
    // Check connection
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    // Set this player status offline.
    if($name === "Tian Wu"){
        $upd = "update user_profile set online = false";
        $res = $conn->query($upd);
    }
    else{
        $upd = "update user_profile set online = false where name = '".$name."'";
        $res = $conn->query($upd);
    }
    echo "Set player offline successfully!";
}
catch(PDOException $e){
    echo "Error: " . "<br>" . $e->getMessage();
}

$conn = null;
?>