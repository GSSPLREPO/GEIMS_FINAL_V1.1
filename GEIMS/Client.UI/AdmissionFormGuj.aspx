<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmissionFormGuj.aspx.cs" Inherits="GEIMS.Client.UI.AdmissionFormGuj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>--%>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	

	<style>
		p{
			font-size: 14px;
		}
	</style>
    <%--</head>
<body>
    <form id="form1" runat="server">
		

		<div class="container">
		<div class="row">
			<div class="col-sm-3">
				<img src="../FertilizerNagarSchool.jpg" style="height: 200px; margin-top:30px;"/>
			</div>
			<div class="col-sm-6" >
                
				<h2 align="center" style="margin-top:60px;"/>ફર્ટીલાઈઝરનગર શાળા</h2>
				<p align="center">મુ.પો.ફર્ટીલાઈઝરનગર, જિ.વડોદરા - 391 750</p>
				<h4 align="center"><span style="border: 2px solid black;padding:7px">નવા પ્રવેશ માટેનું અરજી પત્રક</span></h4>
				
			</div>
			</div>
			</div>

        <div style="margin-left:639px">
        <label for="fname" style="font-size:13px">જી.એસ.અફ.સી.કોડ:&nbsp&nbsp&nbsp__________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">કોડ નંબર અને ગ્રેડ:&nbsp&nbsp&nbsp__________________________________________________</label><br />

            <label for="fname" style="font-size:13px">ધોરણ:&nbsp&nbsp&nbsp_________________________</label><label for="fname"style="font-size:13px">માદયમ:&nbsp&nbsp&nbsp__________________________</label>
            </div >
        
        <div style="margin-top:-75px"><br />
			
        <label for="fname" style="font-size:13px">અરજીપત્ર નંબર :</label>
			<asp:TextBox ID="UserName" runat="server" ToolTip="Enter User Name"></asp:TextBox><br /> 
            
        <label for="fname" style="font-size:13px">યુ.આઈ.ડી નંબર:&nbsp&nbsp&nbsp________________________________</label><br />
            
            <div>

        <label for="fname" style="font-size:13px">૧.બાળકનું નામ:(અંગ્રેજી કેપીટલ અક્ષરો)</label><br />
                
                <p style="font-size:13px">_________________________________________&nbsp&nbsp&nbsp ___________________________________________&nbsp&nbsp&nbsp __________________________________________________&nbsp&nbsp&nbsp___________________________________________________&nbsp&nbsp&nbsp</p>

  <p style="font-size:13px">(અટક)  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp (બાળકનું નામ) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
	  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp(પિતાનું નામ) &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp (માતાનું નામ)</p>
  
                </div>
        <label for="fname" style="font-size:13px">૨.જન્મ તારીખ :(આંકડામાં)&nbsp&nbsp&nbsp_____________________________________________________________________________________________________________________________________________________________________</label><br />

        <label for="fname" style="font-size:13px">(આંકડામાં):&nbsp&nbsp&nbsp___________________________________________________________________________________________________________________________________________________________________________________</label><br />

        <label for="fname" style="font-size:13px">૩.માતૃભાષા:&nbsp&nbsp&nbsp___________________________________________________________________</label><label for="fname" style="font-size:13px">4.જાતી:છોકરો/છોકરી&nbsp&nbsp&nbsp_______________________________________________________________________________________</label><br />
        
        <label for="fname" style="font-size:13px">૫. ધર્મ:&nbsp&nbsp&nbsp_______________________________________________</label><label for="fname" style="font-size:13px">પેટાજ્ઞાતિ:&nbsp&nbsp&nbsp_____________________________________________</label><label for="fname" style="font-size:13px">(SC/ST/BP/GEN):&nbsp&nbsp&nbsp___________________________________________________________</label><br />
        
        
        <label for="fname" style="font-size:13px">૬.જન્મ સ્થળ:&nbsp&nbsp&nbsp(ગામ/શહેર)______________________________________(તાલુકો) _____________________________(જીલ્લો) _____________________________________(રાજ્યો)_____________________________________&nbsp&nbsp&nbsp</p></label>
    

            <label for="fname" style="font-size:13px">૬. વિદ્યાર્થી આધાર કાર્ડ નંબર : &nbsp&nbsp&nbsp________________________________________________________________________________________________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">૬. બેન્કનું નામ : _____________________________________________________ બ્રાન્ચ : _____________________________________________________ એકાઉન્ટ નંબર : _____________________________________________</label><br />

                <label for="fname" style="font-size:13px">પિતા/વાલીનું સંપૂર્ણ રહેઠાણ સરનામું:</label><br />
        <label for="fname" style="font-size:13px">નામ:&nbsp&nbsp&nbsp________________________________________________________________________________________________________________________________________________________________________________________</label><br />
        <label for="fname" style="font-size:13px">સરનામું:&nbsp&nbsp______________________________________________________________________________________________________________________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp________________________________________________________________________________ ટેલીફોન નંબર___________________________________________________પીનકોડ નંબર________________________</label><br />
			
        <label for="fname" style="font-size:13px">પિતા/વાલીનું નોકરી ધંધાનું હોદ્દા સાથેનું સંપૂર્ણ સરનામું ::</label><br />
        <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp________________________________________________________________________________________________________________________________________________________________________________________________</label><br />
        <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp_________________________________________________________________________________________________________________________________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">&nbsp&nbsp&nbsp__________________________________________________________________________________________&nbsp&nbsp&nbsp&nbsp&nbsp_____________________________________________&nbsp&nbsp&nbsp&nbsp&nbsp___________________________________________________</label><br />
            <p style="font-size:13px">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp ટેલીફોન નંબર &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp પીનકોડ નંબર</p>
        <label for="fname" style="font-size:13px">8.બાળકનો અરજદાર સાથેનો સંબંધ (નિશાની કરો):</label> <label for="fname" style="font-size:13px"> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp હાલનું રહેઠાણ:__________________________________________________________________________</label><br />
            <label for="fname" style="font-size:13px">પુત્ર:&nbsp&nbsp&nbsp___________________________</label><label for="fname" style="font-size:13px"> /પુત્રી:&nbsp&nbsp&nbsp___________________________ </label><label for="fname" style="font-size:13px"> /(અન્ય(લખો)&nbsp&nbsp&nbsp_____________________ </label><br />
         
        
        
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
   
       
        1.Fees Details:<br />
       <style>
table, th, td {
  border:1px solid black;
  border-collapse: collapse;
  height:55px
}
</style>
<table style="width:100%">
  <tr>
    <th style="text-align:center">Admission Fee</th>
    <th style="text-align:center">Term Fee</th>
    <th style="text-align:center">Tuition Fee</th>
      <th style="text-align:center">Progress Fee</th>
    <th style="text-align:center">Badge Fee</th>
    <th style="text-align:center">Snack Fee</th>
      <th style="text-align:center">Total</th>
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
</html>--%>


    </head>
<body>
	  <form id="form1" runat="server">
          <table>
		
		<tr>Rs___
			<th>
                <%--<img src="../FertilizerNagarSchool.jpg" style="height: 200px;"/>--%>
				  <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" style="height: 200px;" />
			</th>
			<th>
				<asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" style="margin-left: 677px;"/>
				<p style="margin-top: -10px; margin-left: -130px; font-size: 40px;">ફર્ટીલાઈઝરનગર શાળા</p>
				<p style="padding-left:700px; margin-top:-65px; width: 80px; ">ફોટો</p>
				
				<p style=" margin-left: -120px; font-size:20px;">મુ.પો.ફર્ટીલાઈઝરનગર, જિ.વડોદરા</p>
				<h5 style="margin-left: 40px; margin-right: 200px; padding-left:-6px; border: 3px solid black; border-radius: 25px;">નવા પ્રવેશ માટેનું અરજી પત્રક</h5>
				<p style="padding-left:670px; margin-top:-40px;">તારીખ:-</p>
                <%--<p style="padding-left:850px; margin-top:-35px;">/</p>
				<p style="padding-left:950px; margin-top:-35px;">/</p>
				<p style="padding-left:1000px; margin-top:-35px;">૨૦૨</p>--%>
				
			</th>
		</tr>
	
	</table>

	<table>

		<tr>
			<td>
				<p>અરજીપત્ર નંબર :<asp:TextBox id="TxtFormNo" Text="0" runat="server" BorderStyle="None" OnTextChanged="TxtFormNo_TextChanged" Font-Bold="True" Font-Overline="False" Font-Size="Large" Font-Underline="True" Height="21px" TextMode="Number" Enabled="False" BackColor="White" Width="89px" /><br /> </p>
				<p style="padding-left:765px; margin-top:-60px;">
					જી.એસ.અફ.સી.કોડ
				</p>
				<p style="padding-left:765px;">કોડ નંબર અને ગ્રેડ  ________________________ </p>
				<p> જ.ર.નંબર : _______________ </p>
				<p style="padding-left:220px; margin-top:-35px;">
					યુ.આઈ.ડી નંબર ______________________________
				</p>
				<p style="padding-left:765px; margin-top:-35px;">
					ધોરણ_______________માદયમ_____________
				</p>
				<p>૧. બાળકનું નામ : _________________________(અટક) </p>
				<p style="padding-left:300px; margin-top:-23px;">_______________________(બાળકનું નામ)</p>
				<p style="padding-left:550px; margin-top:-33px;">_______________________(પિતાનું નામ)</p>
				<p style="padding-left:800px; margin-top:-35px;">_______________________(માતાનું નામ)</p>
				<p style="padding-left:17px; margin-top:-15px;">(અંગ્રેજી કેપીટલ અક્ષરો)</p>
                <%--<p style="padding-left:450px; margin-top: -40px; ">(બાળકનું નામ)</p>
				<p style="padding-left:800px; margin-top: -35px; ">(પિતાનું નામ)</p>
				<p style="padding-left:1050px; margin-top: -40px; ">(માતાનું નામ)</p>--%>
				<p>૨. જન્મ તારીખ : (આંકડામાં)_________________________________________________________________</p>
				<p style="padding-left:630px; margin-top:-33px;">માતૃભાષા __________________________________ જાતી:છોકરો/છોકરી</p>
				<p style="padding-left:17px;">જન્મ તારીખ : (સબ્દોમાં)_________________________________________________________________________________________________________________________________</p>
				
				<p>૩. જન્મ સ્થળ : _________________________(ગામ/શહેર) </p>
				<p style="padding-left:320px; margin-top:-33px;">______________________________(તાલુકો)</p>
				<p style="padding-left:573px; margin-top:-33px;">______________________________(જીલ્લો)</p>
				<p style="padding-left:825px; margin-top:-35px;">__________________________(રાજ્યો)</p>
                <%--<p style="padding-left:140px; margin-top:-15px;">(ગામ/શહેર)</p>
				<p style="padding-left:490px; margin-top: -40px; ">(તાલુકો)</p>
				<p style="padding-left:800px; margin-top: -35px; ">(જીલ્લો)</p>
				<p style="padding-left:1080px; margin-top: -40px; ">(રાજ્યો)</p>--%>

				<p>૪. ધર્મ : ________________________________________________________________</p>
				<p style="padding-left:490px; margin-top:-33px;">પેટાજ્ઞાતિ : ___________________________________________________(SC/ST/OBC/GEN/EBC)</p>
                <%--<p style="padding-left:895PX; margin-top:-33px;">(SC/ST/OBC/GEN/EBC)</p>--%>
				<p>૫. વિદ્યાર્થી આધાર કાર્ડ નંબર : ______________________________________________________________________________________________________________________________ </p>
				<p>૬. બેન્કનું નામ : _____________________________________</p>
				<p style="padding-left:350px; margin-top:-33px;">બ્રાન્ચ : ___________________________________________</p>
				<p style="padding-left:700px; margin-top:-35px;">,એકાઉન્ટ નંબર : _____________________________________</p>
				<p>૭. પિતા/વાલીનું સંપૂર્ણ રહેઠાણ સરનામું :</p>
				<p style="padding-left:17px;">નામ:_________________________________________________________________________________________________________________________________________________</p>
				<p style="padding-left:17px;">સરનામું : _____________________________________________________________________________________________________________________________________________ </p>
				<p style="padding-left:17px;">___________________________________________________________________________________________</p>
				<p style="padding-left:700px; margin-top:-35px;">____________________</p>
				<p style="padding-left:900px; margin-top:-35px;">______________________</p>
				<p style="padding-left:750PX;">ટેલીફોન નંબર</p>
				<p style="padding-left:950px; margin-top:-39px;">પીન કોડ નંબર</p>
				<p>૮. પિતા/વાલીનું નોકરી ધંધાનું હોદ્દા સાથેનું સંપૂર્ણ સરનામું :</p>
				<p style="padding-left:17px;">_____________________________________________________________________________________________________________________________________________________</p>
				<p style="padding-left:17px;">_____________________________________________________________________________________________________________________________________________________</p>
				<p style="padding-left:17px;">___________________________________________________________________________________________</p>
				<p style="padding-left:700px; margin-top:-35px;">____________________</p>
				<p style="padding-left:900px; margin-top:-35px;">_______________________</p>
				<p style="padding-left:740PX;">ટેલીફોન નંબર</p>
				<p style="padding-left:950px; margin-top:-39px;">પીન કોડ નંબર</p>
				<p>૯.બાળકનો અરજદાર સાથેનો સંબંધ : (નિશાની કરો)</p>
				<p style="padding-left:400px; margin-top:-40px;" >૧૦.હાલનું રહેઠાણ :________________________________________________________________________________</p>
				<p style="padding-left:17px;">પુત્ર ____________________ / પુત્રી ________________________</p>
				<p style="padding-left:400px; margin-top:-33px;">અન્ય(લખો _______________________________________________________________________________________</p>
				<p>૧૧.પિતા અને માતાની શૈક્ષણિક લાયકાત : (લખો)</p>
				<p style="padding-left:17px;">પિતા ___________________________________ માતા ___________________________________</p>
				<p style="padding-left:680px; margin-top:-33px;" >પિતાનું નામ______________________________________________</p>
				<p>૧૨.છેલ્લે અભ્યાસ કરેલ સાળાનું નામ : ____________________________________________________</p>
				<p style="padding-left:680px; margin-top:-33px;" >પિતાની સહી_____________________________________________</p>
				<p style="padding-left:17px;">(શાળા છોડયાનો દાખલો જોડવો)</p>
				<p style="padding-left:680px; margin-top:-33px;" >માતાનું નામ______________________________________________</p>
				<p>૧૩.આધાર કાર્ડ નંબર : _______________________________________________________________</p>
				<p style="padding-left:680px; margin-top:-33px;" >માતાની સહી_____________________________________________</p>				
				<p>૧૪. બેંક ખાતા નંબર આઈ.અફ.સી.કોડ સાથે : _____________________________________________________________________________________________________________________ </p>
				<p>૧૫.બેંક બ્રાંચ : ______________________________________________________________</p>
				<p style="padding-left:550px; margin-top:-33px;" >ફોર્મ સ્વીકાર્યાની તારીખ :_____________________________________________________</p>
				------------------------------------------------------------------------------------------(ફક્ત કાર્યાલય માટે)-----------------------------------------------------------------------------------------------------------------
				
				<p>૧.ફી વિગત :</p>
				<table border="1" cellspacing="0" style="width: 1055px;" >
					<tr>
						<th>પ્રવેશ ફી</th>
						<th>સત્ર ફી</th>
						<th>શિક્ષણ ફી</th>
						<th>સિક્કા ફી</th>
						<th>પ્રગતિ પત્રક ફી</th>
						<th>નાસ્તા ફી</th>
						<th>ફૂલ ફી</th>
					</tr>
					<tr style="height:30px;" >
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
					</tr>

				</table>
				<p>૨.રસીદ નંબર ______________________________</p>
				<p style="padding-left:400px; margin-top:-33px;">કેટેગરી તપાસનારની સહી______________________________</p>
				<p style="padding-left:850px; margin-top:-33px;">તારીખ_______________________________________</p>
				<p>૩.બાળકની જન્મ તારીખ _________________________</p>
				<p style="padding-left:400px; margin-top:-33px;">જન્મ તારીખ તપાસનારની સહી _________________________</p>
				<p style="padding-left:850px; margin-top:-33px;">તારીખ_______________________________________</p>
				<p>૪.બાળકને દાખલ કર્યું/દાખલ ન કર્યુ વડાની સહી તારીખ ___________________________________________________________________________________________________________</p>
				<p align="center">(મુ.શિ/આચાર્ય/એડમિનિસ્ટ્રેટર)</p>
				<p align="center">(વર્ગ શિક્ષકની નોંધ)</p>
				<p>કુમાર/કુમારી _______________________________________ નું નામ મારા ધોરણ/વર્ગ __________ માં દાખલ કરેલ છે અને તેનું નામ મારા વર્ગના રજીસ્ટ્રરમાં ઉમેરેલ છે.,</p>
				<p>તારીખ :__________વર્ગ શિક્ષકની સહી______________________________</p>
				   <br><br><br><br><br><br>

												<!-- 2nd page -->

				<p style="text-align:center;margin-left: -548px;font-size:18px;border: 2px solid black;border-radius: 25px;margin-left: 78px; margin-right: 670px; padding-left:-6px;">કે.જી. પ્રાથમિક અને માધ્યમિક વિભાગ માટેના નિયમો </p>
				<p>૧. બાળક ની લોઅર કે.જી. માટેના પ્રવેશ માટે જે વર્ષમાં પ્રવેશ લેવાનો હોય તે વર્ષની ૩૧મી મેં સુધીમાં તેની ઉમર ૩ વર્ષ પુરા <br>થયે
					લા  હોવા જોઈએ અને અપર કે.જી. માટે, જે વર્ષમાં પ્રવેશ લેવાનો હોઈ તે વર્ષની ૩૧મી મે સુધીમાં તેની  ઉમર ૪ વર્ષ પુરા <br>થયેલા  હોવા જોઈએ . </p>
				<p>૨. ફી એકવાર શાળામાં ચૂકવ્યા બાદ કોઈ પણ સંજોગોમાં પરત કરવામાં આવશે નહિ.</p>
				<p>૩. બાળકનું પ્રવેશપત્ર ફોર્મ જમા કરાવવાથી  બાળકને શાળામાં પ્રવેશ મળશે જે તેની ખાત્રી નથી.</p>
				<p>૪. બાળક શાળામાં જો કોઈપણ જાતની અગાઉથી જાણ કર્યા વગર અથવા કોઈ પણ યોગ્ય કારણ વગર ૧ મહિનાથી વધારે <br>સમય ગેરહાજર રહેશે તો શાળાના પત્રકમાંથી જાણ કર્યા વગર તેનું નામ કમી  કરવામાં આવશે. </p>
				<p>૫. જી .અસ.અફ.સી કંપનીના કર્મચારીઓને વિનંતી કરવામાં આવે છે કે પ્રવેશ્પત્રક ફોર્મ સાથે પોતાના ઓળખકાર્ડની ઝેરોષ<br>નકલ જોડવી. </p>
				<p>૬.શાળાનો સમય </p>
				<p style="padding-left:17px;">અ) કે .જી . વિભાક માટે :(અંગ્રેજી  તથા ગુજરાતી વિભાગ)</p>
				<p style="padding-left:39px; margin-top:-15px;">સોમવાર થી સુક્રવાર : ૮.૦૦(સવારે) થી ૧૧.૦૦(સવારે) </p>
				<p style="padding-left:39px; margin-top:-15px;">શનિવાર કે.જી. વિભાક બાળકો માટે બંધ રહેશે   </p>

				<p style="padding-left:17px;">બ) પ્રાથમિક વિભાક માટે :(ગુજરાતી/અંગ્રેજી વિભાગ)</p>
				<p style="padding-left:39px; margin-top:-15px;">સોમવાર થી સુક્રવાર : ૧૨.૨૫ (બાપોરે) થી ૫.૪૯(સાંજે) </p>
				<p style="padding-left:39px; margin-top:-15px;">શનિવાર: ૭.૩૦(સવારે) થી ૧૦.૪૦(સવારે)   </p>
				<p style="padding-left:17px;">ક)સેકન્ડરી/હાયર સેકન્ડરી વિભાક માટે :(ગુજરાતી તથા અંગ્રેજી  વિભાગ)</p>
				<p style="padding-left:39px; margin-top:-15px;">સોમવાર થી સુક્રવાર : ૭.૧૫(સવારે) થી ૧૨.૨૦(બપોરે) </p>
				<p style="padding-left:39px; margin-top:-15px;">શનિવાર:૧૧.૦૦(બપોરે) થી ૨.૪૦(બપોરે)   </p>
				<p>૭. ગણવેશ :</p>
				<p style="padding-left:17px;">અ) ધોરણ ૮ થી ૧૨ માટે (અંગ્રેજી તથા ગુજરાતી વિભાજ)</p>
				<p style="padding-left:40px;" >છોકરાઓ માટે </p>
				<p style="padding-left:230px; margin-top:-38px;" >છોકરરીંઓ માટે </p>
				<p style="padding-left:40px;" >ફૂલ પેન્ટ : ગ્રે </p>
				<p style="padding-left:230px; margin-top:-38px;" >પીના :ગ્રે  </p>
				<p style="padding-left:40px;" >શેર્ટ: સફેદ </p>
				<p style="padding-left:230px; margin-top:-38px;" >બ્લોઉચ:સફેદ </p>
				<p style="padding-left:40px;" >મોજા :સફેદ  </p>
				<p style="padding-left:230px; margin-top:-38px;" >મોજા :સફેદ  </p>
				<p style="padding-left:40px;" >બૂટ :કાળા  </p>
				<p style="padding-left:230px; margin-top:-38px;" >બૂટ :કાળા  </p>
				<p>૮.ધોરણ ૯/૧૦(માધ્યમિક)વિભાગમાં એઙમીશન વખતે જોડવાની ઝેરોષ યાદી :</p>
				<p style="padding-left:15px; margin-top:-15px;" >૧)ધોરણ ૯ માં એડમીસન માટે ધોરણ ૮ ની માર્કશીટ અને ધો. ૧૦ એડમીસન માટે ધોરણ ૯ની માર્કશીટ નકલ  </p>
				<p style="padding-left:15px; margin-top:-15px;">૨.SC/ST/BAXI/જાતિના વિદ્યાર્થીઓં માટે પિતાનું અને જાતીપ્રમાણપત્ર અને રહેઠાણના પુરાવાની નકલ(બી .પી એલ<br>કાર્ડ,આધાર કાર્ડ )</p>
				<p style="padding-left:15px; margin-top:-15px;">૩.SC/ST/BAXI/જાતિના વિદ્યાર્થીઓં માંમલતદારશ્રીનો આવકનો દાખલો આપવો.</p>

				<%--<a href="C:\Users\User\Desktop/Gujarati.pdf" download="Gujarati" target="_blank">
				<button style="width: 150px; height: 30px; background-color: orangered; color: white; border:2px solid orangered; font-size:18px; font-family: sans-serif; border-radius: 10px;">Download PDF</button></a>--%> 
	 </form>
</body>
</html>

