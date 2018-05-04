<?php

$Server_path = "/var/www/КСиС/FILES/";

function print_files($handle, $path)
{
    while (false !== ($entry = readdir($handle))) {
        if( ($entry != "." && $entry != "..")) {
            if (is_dir($path . "/" . $entry)) {
                //echo $path . "/" . $entry . "<br/>";
                $temp = opendir($path . "/" . $entry);
                print_files($temp, $path . "/" . $entry);
                closedir($temp);
            } else {
                $_temp = $path;
                $_temp = str_replace("/var/www/КСиС/FILES/", '~', $_temp);
                echo "<tr><td>$_temp</td><td>$entry</td><td>" . filesize($path . "/" . $entry) . "</td><td>".date("F d Y H:i:s.",filectime($path . "/" . $entry))."</td></tr>";
            }
        }
    }
}

if(isset($_POST["showDirectories"]))
{
    echo "<table border='1'><tr><th>PATH</th><th>NAME</th><th>SIZE</th><th>CREATED</th></tr>";
    $handle = opendir($Server_path);
    print_files($handle, $Server_path);
    closedir($handle);
    echo "</table>";
}

if(isset($_POST["addFile"]))
{
    $upload_dir = $Server_path . $_POST["dir"];
    $upload_file = $upload_dir . basename($_FILES["loadFile"]["name"]);
    if (move_uploaded_file($_FILES['loadFile']['tmp_name'], $upload_file)) {
        chmod($upload_file, 0777);
        echo "Done";
    } else
        echo "Error(";
}

if(isset($_POST["readFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        echo file_get_contents($path);
    }
    else
        echo "File does not exist";
}

if(isset($_POST["deleteFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        if(unlink($path))
            echo "Deleted";
        else
            echo "Error(";
    }
    else
        echo "Error(";
}

if(isset($_POST["mkDirectory"]))
{
    $path = $Server_path . $_POST["dir"];
    mkdir($path, 0777, true);
    chmod($path, 0777);
    echo "Created";
}

if(isset($_POST["editFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        file_put_contents($path, $_POST["temp_dir"], FILE_APPEND);
        echo "Done";
    }
    else
        echo "Error(";
}

if(isset($_POST["rewriteFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        file_put_contents($path, $_POST["temp_dir"]);
        echo "Done";
    }
    else
        echo "Error(";
}

if(isset($_POST["copyFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        $newPath = $Server_path . $_POST["temp_dir"];
        if(is_dir($newPath))
        {
            copy($path, $newPath . basename($path));
            echo "Done";
        }
        else
            echo "Error(";
    }
    else
        echo "Error(";
}

if(isset($_POST["removeFile"]))
{
    $path = $Server_path . $_POST["dir"];
    if(is_file($path))
    {
        $newPath = $Server_path . $_POST["temp_dir"];
        if(is_dir($newPath))
        {
            copy($path, $newPath . basename($path));
            unlink($path);
            echo "Done";
        }
        else
            echo "Error(";
    }
    else
        echo "Error(";
}

if(isset($_POST["downloadFile"])) {
    $file = $Server_path . $_POST["dir"];
    if (file_exists($file)) {
        header('Content-Description: File Transfer');
        header('Content-Type: application/octet-stream');
        header('Content-Disposition: attachment; filename="' . basename($file) . '"');
        header('Expires: 0');
        header('Cache-Control: must-revalidate');
        header('Pragma: public');
        header('Content-Length: ' . filesize($file));
        readfile($file);
        exit;
    }
    else
        echo "Error(";
}