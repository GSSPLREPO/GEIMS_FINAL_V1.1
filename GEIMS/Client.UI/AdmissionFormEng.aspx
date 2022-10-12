<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmissionFormEng.aspx.cs" Inherits="GEIMS.Client.UI.AdmissionFormEng" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<meta charset="utf-8"/>
    
 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<img src="../FertilizerNagarSchool.jpg" width="50" height="80" style="margin-top:40px"/>
        <div>

            <p style="text-align:center;font-size: 27px"><b>FERTILIZERNAGAR SCHOOL</b>

            </p>
			
		
             <p style="text-align:center">P.O FERTILIZERNAGAR, DIST,VADODARA-391 750
            </p>
             <p style="text-align:center;font-size: 18px"><span style="border: 1px solid black"><b>APPLICATION FORM FOR ADMISSION</b></span>
            </p>
        </div>--%>

		<div class="container">
		<div class="row">
			<div class="col-sm-3">
				<asp:Image ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" runat="server" ID="imgphoto"
                                                Width="150px" Height="150px" style=" margin-top:110px " BorderStyle="None"  ImageAlign="Middle" />
			</div>
			<div class="col-sm-3 col-sm-6" >
				<asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" style="margin-left: 466px;"/> <br />
				<p style="margin-left: 566px; margin-top:12px">Rs___</p>
				<h2 align="center" style="margin-top:60px;"/>FERTILIZER NAGAR SCHOOL</h2>
				<p align="center">P.O.FERTILIZERNAGAR,DIST. VADODARA - 391 750 </p>
				<h4 align="center"><span style="border: 2px solid black;padding:7px">APPLICATION FORM FOR ADMISSION</span></h4><br />
				
			</div>
			</div>
			</div>

        <div style="margin-left:639px">
        <label for="fname" style="font-size:13px">GSFC Emp. Code No:&nbsp&nbsp&nbsp__________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">&Grade:&nbsp&nbsp&nbsp________________________________________________________________</label><br />

            <label for="fname" style="font-size:13px">Medium:&nbsp&nbsp&nbsp_________________________</label><label for="fname"style="font-size:13px">Standard:&nbsp&nbsp&nbsp__________________________</label>
            </div >
        
        <div style="margin-top:-75px"><br />
			
        <label for="fname" style="font-size:13px">Form No:</label>
			<asp:TextBox id="TxtFormNo" Text="0" runat="server" BorderStyle="None" OnTextChanged="TxtFormNo_TextChanged" Font-Bold="True" Font-Overline="False" Font-Size="Large" Font-Underline="True" Height="21px" TextMode="Number" Enabled="False" BackColor="White" Width="89px" /><br /> 

            
        <label for="fname" style="font-size:13px">G.R.No:&nbsp&nbsp&nbsp________________________________</label><br />
            
            <div>

        <label for="fname" style="font-size:13px">1.Full Name of the ward:(In Block Letters)</label><br /><br />
                
                <p style="font-size:13px">_________________________________________&nbsp&nbsp&nbsp ___________________________________________&nbsp&nbsp&nbsp __________________________________________________&nbsp&nbsp&nbsp___________________________________________________&nbsp&nbsp&nbsp</p>

  <p style="font-size:13px">(SurName)  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp (Name) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
	  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp(Father's Name) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp (Mother's Name)</p>
  
                </div>
        <label for="fname" style="font-size:13px">2.Date of Birth:(in Figure)&nbsp&nbsp&nbsp____________________________________________________________________________________________________________________________________________________________________</label><br />

        <label for="fname" style="font-size:13px">(In Words):&nbsp&nbsp&nbsp___________________________________________________________________________________________________________________________________________________________________________________</label><br />

        <label for="fname" style="font-size:13px">3.Mother Tonque:&nbsp&nbsp&nbsp___________________________________________________________________</label><label for="fname" style="font-size:13px">4.Sex:Male/Female&nbsp&nbsp&nbsp___________________________________________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">4.Religion:&nbsp&nbsp&nbsp_______________________________________________</label><label for="fname" style="font-size:13px">Sub Caste:&nbsp&nbsp&nbsp_____________________________________________</label><label for="fname" style="font-size:13px">(SC/ST/BP/GEN):&nbsp&nbsp&nbsp_______________________________________________________</label><br />
        
        
        <label for="fname" style="font-size:13px">5.Birth Place:&nbsp&nbsp&nbsp____________________________________________&nbsp&nbsp&nbsp _______________________________________&nbsp&nbsp&nbsp _____________________________________________&nbsp&nbsp&nbsp__________________________________________&nbsp&nbsp&nbsp</p></label>
            <%--   <p style="font-size:13px">___________________________________&nbsp&nbsp&nbsp ________________________________&nbsp&nbsp&nbsp _____________________________________&nbsp&nbsp&nbsp_____________________________&nbsp&nbsp&nbsp</p>--%>

  <p style="font-size:13px">(Village/Town) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
	  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp(Taluka)
	  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
	  (District) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp (State)</p>

                <label for="fname" style="font-size:13px">Full Name and Address of Father/ Gurardian:</label><br />
        <label for="fname" style="font-size:13px">Name:&nbsp&nbsp&nbsp________________________________________________________________________________________________________________________________________________________________________________________</label><br />
        <label for="fname" style="font-size:13px">Address:&nbsp&nbsp_______________________________________________________________________________________________________________________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp________________________________________________________________________________ &nbsp&nbsp&nbsp___________________________________________________&nbsp&nbsp&nbsp________________________________________________</label><br />
			  <p style="font-size:13px"> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
				  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Tel.No
	  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Pine Code</p>

        <label for="fname" style="font-size:13px">Full Address of the Father/Guardian's service/Business address with designation:</label><br />
        <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp________________________________________________________________________________________________________________________________________________________________________________________________</label><br />
        <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp_________________________________________________________________________________________________________________________________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp__________________________________________________________________________________________&nbsp&nbsp&nbsp&nbsp&nbsp_____________________________________________&nbsp&nbsp&nbsp&nbsp&nbsp___________________________________________________</label><br />
            <p style="font-size:13px">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Tel.No &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Pine Code</p>
        <label for="fname" style="font-size:13px">8.Applicant's relation with Child:</label><br />
            <label for="fname" style="font-size:13px">SON:&nbsp&nbsp&nbsp____________________________________________</label><label for="fname" style="font-size:13px"> /DAUGHTER:&nbsp&nbsp&nbsp____________________________________________________ </label><label for="fname" style="font-size:13px"> /OTHER(PI.specify)&nbsp&nbsp&nbsp______________________________________________________ </label><br />
         
        
        
        <label for="fname" style="font-size:13px">Educational Qualification of Parents:(PI.Write)&nbsp&nbsp&nbsp</label><br />
        <label for="fname" style="font-size:13px">Father:&nbsp&nbsp&nbsp_____________________________________________________________________________________</label><label for="fname" style="font-size:13px">/Mother:&nbsp&nbsp&nbsp_________________________________________________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">Name of the Last School Attended:&nbsp&nbsp&nbsp___________________________________________________________________________________________________________________________________________________________</label><br />
        <label for="fname"style="font-size:13px">(Please attach School Leaving Certificate)&nbsp&nbsp&nbsp</label><br />
        <label for="fname" style="font-size:13px">Date of Submission&nbsp&nbsp&nbsp_____________________________________________</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<label for="fname">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp____________________________________________________________________________________</label><br />
         <p style="font-size:13px">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
             &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
             &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
             &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp(Signature of Parent/Guardian) </p>
            </div>
        <p>--------------------------------------------------------------(FOR OFFICE USE ONLY)--------------------------------------------------------------------------</p>
   
        <%--<p style="text-align:center">(FOR OFFICE USE ONLY)</p>--%>1.Fees Details:<br /><br />
       <style>
table, th, td {
  border:1px solid black;
  border-collapse: collapse;
  height:55px
}
</style>
<table style="width:100%; text-align:center">
  <tr>
    <th>Admission Fee</th>
    <th>Term Fee</th>
    <th>Tuition Fee</th>
      <th>Progress Fee</th>
    <th>Badge Fee</th>
    <th>Snack Fee</th>
      <th>Total</th>
  </tr>
    
  <tr>
    <td></td>
    <td></td>
    <td></td>
        <td></td>
    <td></td>
    <td></td>
       <td></td>
  </tr>

</table><br />

       <div>
         <label for="fname" style="font-size:13px">2.Receipt No&nbsp&nbsp&nbsp____________________________________________________</label><label for="fname" style="font-size:13px">Date&nbsp&nbsp&nbsp___________________________________________</label><label for="fname" style="font-size:13px">Sign for verifying category:&nbsp&nbsp&nbsp______________________________________________</label><br />
        
        
         <label for="fname" style="font-size:13px">3.Date of Birthday Of Student&nbsp&nbsp&nbsp_________________________________________________________</label><label for="fname" style="font-size:13px">Sign for verifying Date of Birth:&nbsp&nbsp&nbsp______________________________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">4.Authorized Signatory:&nbsp&nbsp&nbsp________________________________________________________________</label><label for="fname" style="font-size:13px">Date:&nbsp&nbsp&nbsp________________________________________________________________________________________________</label>
		</div><br />

        <h4 align="center">RULES FOR K.G. PRIMARY AND SECONDARY SECTION</h4><br />
	    	
	    

	    
	    		<p>1.A child can be admitted in</p>
	    		<p >Lower K.G. at the age of 3+ As on the 31st August of the Academic Year.</p>
	    		<p >Upper K.G. at the age of 4+ As on the 31st August of the Academic Year.</p>
	    		<p >2.Parents are requested to bring their child when called for meeting,please also bring Birth</p>
	    		<p >Certificates,School Leaving Certificate etc</p><p>3.Fees once paid will not be refunded in any case.</p><p>4.Submission of Application form does not mean that admission is secured.</p>
	    		<p>5.If the child remains absent for more than one month without intimation of valid reason</p>
	    		<p >His/her name will be struck off from school register.</p>
	    		<p >6.GSFC Company Employee is requested to attach photocopy of identity card.</p>
	    		<h4>7.SCHOOL TIME :</h4>
	    		<h4>a.FOR K.G. SECTION : (ENGLISH AND GUJARATI MEDIUM)</></h4>
	    		<p>Monday to Friday : 8.00 am to 11.00 am</p>
	    		<h4>b.FOR PRIMARY SECTION : (ENGLISH AND GUJARATI MEDIUM)</h4>
	    		<p >Monday to Friday : 12.35 pm to 5.25 pm</p>
	    		<p >Saturday : 7.30 am to 10.40 am</p>
	    		
            
                <h4>8.UNIFORM</h4>
	    		<h4>a.FOR K.G. SECTION : (ENGLISH AND GUJARATI MEDIUM)</h4>
                <div>
                <h4>FOR BOYS &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp FOR GIRLS</h4>
	    		<p>Half Paint : Navy Blue&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Frock:Red Fancy Checks</p>
                    <p>Shirt : Red Fancy Checks&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</p>
                    <p>Socks : Navy Blue&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Socks : Navy Blue</p>
                     <p>Shoes : Navy Blue&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Shoes : Navy Blue</p>

                    <h4>b.FOR 1 TO 7 STANDARD : (ENGLISH AND GUJARATI MEDIUM)</h4>
                
                <h4>FOR BOYS &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp FOR GIRLS</h4>
	    		<p>Half Paint : Maroon&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Pina:Maroon</p>
                    <p>Shirt : White&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Shirt:White</p>
                    <p>Socks : White&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Socks : White</p>
                     <p>Shoes : Black&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Shoes : Black-White Shoes.P.T.on saturday</p>

                     <h4>c.FOR 8 TO 12 STANDARD : (ENGLISH AND GUJARATI MEDIUM)</h4>
                
                <h4>FOR BOYS &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp FOR GIRLS</h4>
	    		<p>Full Paint : Gray&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Pina:Gray</p>
                    <p>Shirt : White&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Shirt:White</p>
                    <p>Socks : White&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Socks : White</p>
                     <p>Shoes : Black&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Shoes : Black</p>
                    </div>
                    
               
  
    </form>

</body>
</html>
