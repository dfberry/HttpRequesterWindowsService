## Request WebPage, Verify Status Code
## http://technet.microsoft.com/en-us/library/ee176949.aspx
Add-Type -Assembly System.Web
$url = "http://dfb-dashboard.elasticbeanstalk.com/Verify/Index/?build=true&feeds=true"

$text = (new-object System.Net.WebClient).DownloadString($url)

$text
