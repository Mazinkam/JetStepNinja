<?php
        // Configuration
        $hostname = 'localhost';
        $username = 'root';
        $password = 'root';
        $database = 'jsn';

        $secretKey = "mySecretKey"; // Change this value to match the value stored in the client javascript below
        $hash = $_GET['hash'];
        echo md5($_GET['name'] . $_GET['score'] . $secretKey);


        try {
            $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
        } catch(PDOException $e) {
            echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
        }

        $realHash = md5($_GET['name'] . $_GET['score'] . $secretKey);
        echo "</br>".$realHash;
        if($realHash == $hash) {
          $name = $_GET['name'];
          $score = $_GET['score'];
            $sth = $dbh->prepare('INSERT INTO scores (id,name,score) VALUES (:id,:name,:score)');
            try {
                $sth->execute(array(
                  'id' => null,
                  'name' => $name,
                  'score' => $score
                ));
                echo "executed!";
            } catch(PDOException $e) {
                echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
            }
        }
?>
