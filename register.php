<?php
$servername = "localhost";
$username = "root";
$password = "a2001360095";
$db = "mmo_shooting";
$user = $_POST['user'];
$pword = $_POST['pword'];
$name = $_POST['name'];

try{
    // Create connection
    $conn = new PDO("mysql:host=$servername;dbname=$db", $username, $password);

    // Check connection
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $sel = "SELECT * from user_profile where username = '".$user."'";
    $res = $conn->query($sel);
    // Check if the username exists
    if($res->rowCount() === 0){
        $sel = "SELECT * from user_profile where name = '".$name."'";
        $res = $conn->query($sel);
        // Check if the name exists
        if($res->rowCount() === 0){
            $ins = "INSERT INTO user_profile (username, password, name, online) VALUES ('".$user."', '".$pword."', '".$name."', false)";
            $res = $conn->query($ins);
            $prompt = "Welcome! " . $name . ".";
            $check = 1;
            $arr = array('prompt' => $prompt, 'check' => $check);
            echo json_encode($arr);
        }
        else{
            $prompt = "Name already exits.";
            $check = 0;
            $arr = array('prompt' => $prompt, 'check' => $check);
            echo json_encode($arr);
        }
    }
    else{
        $prompt = "Username already exits.";
        $check = 0;
        $arr = array('prompt' => $prompt, 'check' => $check);
        echo json_encode($arr);
    }
}
catch(PDOException $e){
    $prompt = "Error: " . "<br>" . $e->getMessage();
    $check = 0;
    $arr = array('prompt' => $prompt, 'check' => $check);
    echo json_encode($arr);
}

$conn = null;
?>