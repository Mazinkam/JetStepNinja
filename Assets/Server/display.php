<?php
    // Configuration
    $hostname = 'localhost';
    $username = 'root';
    $password = 'root';
    $database = 'jsn';

    try {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
    } catch(PDOException $e) {
        echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }

    $sth = $dbh->query('SELECT * FROM scores ORDER BY score DESC LIMIT 5');
    $sth->setFetchMode(PDO::FETCH_ASSOC);

    $result = $sth->fetchAll();

    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['name'], "\t", $r['score'], "\n";
        }
    }
?>
