/*****************************************************************************************************
Created By: Ferdous Md. Jannatul, Sr. Software Engineer
Created On: 10 December 2005
Last Modified: 13 April 2006
******************************************************************************************************/
//Generating Pop-up Print Preview page
function getPrint(print_area) {
    //Creating new page
    //alert('hi');
    var pp = window.open();
    //Adding HTML opening tag with <HEAD> … </HEAD> portion
    pp.document.writeln('<HTML><HEAD><title>Fertilizer Nagar School</title>')
    pp.document.writeln('<LINK href=../_CSS/PrintStyle.css  type="text/css" rel="stylesheet" media="print"> <LINK href="../_CSS/main.css" rel="stylesheet" type="text/css"><base target="_self"><script type="text/javascript">function hidePrint(){  document.getElementById("CLOSE").style.display="none";document.getElementById("PRINT").style.display="none";window.print();}function ShowPrint(){  document.getElementById("PRINT").style.display="Block";document.getElementById("PRINT").style.float="Left";document.getElementById("CLOSE").style.display="Block";}</script></HEAD>');
    pp.document.writeln('<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" >');
    //Adding form Tag
    pp.document.writeln('<form  method="post">');
    pp.document.writeln('<TABLE width=100% ><TR><TD></TD></TR><TR><TD align=center><table><tr><td><INPUT ID="PRINT" style="font-family:verdana;font-size:11px;font-weight:bold" type="button" value="Print" onclick="javascript:location.reload(false);javascript:hidePrint(); javascript:ShowPrint();"></td><td><INPUT ID="CLOSE" type="button" style="font-family:verdana;font-size:11px;font-weight:bold" value="Close" onclick="window.close();"></td></tr></table></TD></TR><TR><TD></TD></TR><TR><TD align=center>' + document.getElementById(print_area).innerHTML + '</TD><script type="text/javascript"></script></TR></TABLE>');
    pp.document.writeln('</form></body></HTML>');
}		
		