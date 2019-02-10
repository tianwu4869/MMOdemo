<?php
$servername = "localhost";
$username = "root";
$password = "a2001360095";
$db = "mmo_shooting";
$user = $_POST['user'];
$pword = $_POST['pword'];

try{
    // Create connection
    $conn = new PDO("mysql:host=$servername;dbname=$db", $username, $password);
    // Check connection
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    // Check if the username exists
    $sel = "SELECT * from user_profile where username = '".$user."'";
    $res = $conn->query($sel);
    if($res->rowCount() === 1){
        $realPassword = "";
        $name = "";
        $online = true;
        foreach ($res as $rows) {
            $realPassword = $rows['password'];
            $name = $rows['name'];
            $online = $rows['online'];
        }
        // Check if the user is already online
        if($online){
            $prompt = "User is online.";
            $arr = array('prompt' => $prompt);
            echo json_encode($arr);
        }
        else{
            // Check if the password is correct
            if(strcmp($realPassword, $pword) === 0){
                // Update user's state
                $upd = "Update user_profile set online = true where username = '".$user."'";
                $res = $conn->query($upd);
                $prompt = "Welcome back! " . $name . ".";
                $arr = array('prompt' => $prompt, 'name' => $name);
                echo json_encode($arr);
            }
            else{
                $prompt = "Wrong password!";
                $arr = array('prompt' => $prompt);
                echo json_encode($arr);
            }
        }    
    }
    else{
        $prompt = "Username does not exit.";
        $arr = array('prompt' => $prompt);
        echo json_encode($arr);
    }
}
catch(PDOException $e){
    $prompt = "Error: " . "<br>" . $e->getMessage();
    $arr = array('prompt' => $prompt);
    echo json_encode($arr);
}

$conn = null;
?>