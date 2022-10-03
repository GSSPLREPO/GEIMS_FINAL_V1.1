<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Master/SchoolMain.Master" CodeBehind="ClasswiseStudentTemplate.aspx.cs" Inherits="GEIMS.Client.UI.ClasswiseStudentTemplate" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../CSS/TabPanel.css" rel="stylesheet" />
	<link href="../CSS/screen.css" rel="stylesheet" />
	<script type="text/javascript">
	    function Clear() {
	        // alert("Clear");
	        // $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
	        //  $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
	        bindClass();
	    }

	    function BindDorpdownOnButtonClick() {
	        $(document.getElementById('<%= ddlClass.ClientID %>')).empty();

		    $.ajax({
		        type: "POST",
		        contentType: "application/json; charset=utf-8",
		        url: "Class_Template.aspx/LoadClass",
		        data: "{'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    // alert("Suceess");
                    var temp = $.parseJSON(data.d);
                    var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);
        		    $("#divLoading").show();
        		    $.each(temp, function (i) {
        		        var optionhtml = '<option value="' +
                         temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
        		        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                	});
        		    $("#divLoading").hide();
        		    $(document.getElementById('<%= ddlClass.ClientID %>')).val($(document.getElementById('<%= hfCLassMID.ClientID %>')).val());

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Class_Template.aspx/LoadDivision",
                        data: "{'intClassMID':'" + $(document.getElementById('<%= hfCLassMID.ClientID %>')).val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                	    dataType: "json",
                	    success: function (data) {
                	        var temp = $.parseJSON(data.d);
                	        $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
                    	    $("#divLoading").show();
                    	    $.each(temp, function (i) {

                    	        var optionhtml = '<option value="' +
                                 temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                    	        $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                        	});
                    	    $("#divLoading").hide();
                    	    $(document.getElementById('<%= ddlDivision.ClientID %>')).val($(document.getElementById('<%= hfDivisionTID.ClientID %>')).val());
                    	},
                	    error: function (error) {
                	        // alert("Error" + error);
                	    }

                	});

        		},
                error: function (error) {
                    // alert("Error" + error);
                }

            });

        }


        function BindCheckBOX() {
            $('[id$=chkHeader]').click(function () {

                $("[id$=chkChild]").attr('checked', this.checked);
            });
            $("[id$=chkChild]").click(function () {
                if ($('[id$=chkChild]').length == $('[id$=chkChild]:checked').length) {
                    $('[id$=chkHeader]').attr("checked", "checked");
                }
                else {
                    $('[id$=chkHeader]').removeAttr("checked");
                }
            });
        }


        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $("#divLoading").hide();
        });

            $(document).ready(function () {
                var Year = document.getElementById('<%=ddlAcademicYear.ClientID%>').value;
			    $(".autosuggest").autocomplete({
			        source: function (request, response) {
			            $.ajax({
			                type: "POST",
			                contentType: "application/json; charset=utf-8",
			                url: "ClasswiseStudentTemplate.aspx/GetAllStudentNameForReport",
			                data: "{'prefixText':'" + request.term + "','SchoolMID':'" +<%=Session[ApplicationSession.SCHOOLID] %> + "','ClassMID':'" + <%= hfCLassMID.Value %> +"','DivisionTID':'" + <%= hfDivisionTID.Value %> +"','AcademicYear': '" + Year + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    response($.map(data.d, function (item) {
                                        //alert();
                                        $("#<%=hfSearchID.ClientID %>").val(item.split('~')[1]);
            				            return {
            				                label: item.split('~')[0],
            				                val: item.split('~')[1]
            				            };

            				        }));
            				        }
            				        else {
            				            alert('No Record Found');
            				        }
            				},
                            error: function () {
                                alert("Error");
                            }
                        });
                    },
                    select: function (e, i) {
                        $("#<%=hfSearchName.ClientID %>").val(i.item.label);
            	    }
                });
			});

	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			ClassWise Student Template
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			<asp:LinkButton CausesValidation="false" ID="lnkAddNewFee" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewFee_Click">Add New</asp:LinkButton>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
					<%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
				</div>

				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
					<ul>
						<li><a id="tabClassTemplateDetails" href="#tabs-1">ClassWise Student Template</a></li>

					</ul>
					<div id="tabs-1" style="min-height: 150px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
						<asp:HiddenField ID="hdnSchoolMID" runat="server" ClientIDMode="Static" />
						<asp:HiddenField ID="hfTab" runat="server" ClientIDMode="Static" />
						<asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
						<asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
						<div style="width: 100%;" class="mydiv">
							<div style="width: 100%;" class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Class :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left;">
									<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlClass" Width="150px">
									</asp:DropDownList>
								</div>

							</div>
							<div style="width: 100%;" class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Division :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left; vertical-align: top;">
									<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision" Width="150px">
									</asp:DropDownList>
								</div>
							</div>

							<div style="width: 100%;" class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Academic Year :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 31%; float: left;">
									<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Width="150px">
									</asp:DropDownList>
								</div>
							</div>
							<div style="width: 100%;" class="divclasswithfloat">

								<div style="text-align: left; width: 50%; float: right;" class="label">
									<asp:Button runat="server" ID="btnViewGrid" Text="View" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />
									<%-- <button id="btnViewGrid" type="button" class="btn-blue btn-blue-medium">View</button>--%>
								</div>
							</div>
							<div id="divLoading" align="center" class="divclasswithfloat">
								Loading. Please wait.<br />
								<br />
								<img src="../Images/loading.gif" alt="" />
							</div>
							<div style="width: 100%;" class="divclasswithfloat" id="divStName" runat="server">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Student Name :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 31%; float: left;">
									<asp:TextBox runat="server" CssClass="textarea autosuggest" ID="TextBox1" Width="150px">
									</asp:TextBox>
									<asp:Button runat="server" ID="btnShow" Text="Show Data" CssClass="btn-blue btn-blue-medium" OnClick="btnShow_Click" />
									<asp:HiddenField runat="server" ID="hfSearchName" />
									<asp:HiddenField runat="server" ID="hfSearchID" />
								</div>
							</div>
							<%-- <div style="width: 100%;" class="divclasswithfloat">
                                <div style="height:200px; width:250px;border:1px solid red;"><img id="swapImg" src="../Images/loading.gif"></div>
                                 </div>--%>
							<div class="clear"></div>
							<div class="divclasswithoutfloat">

								<asp:GridView ID="gvFees" runat="server" AutoGenerateColumns="False"
									BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
									Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true">
									<FooterStyle BackColor="White" ForeColor="#333333" />
									<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
									<Columns>
										<asp:BoundField DataField="FeesCategoryMID" HeaderText="FeesCategory ID">
											<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
											<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
										</asp:BoundField>
										<asp:BoundField DataField="ClassWiseFeesTemplateTID" HeaderText="ClassWiseFeesTemplateTID">
											<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
											<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
										</asp:BoundField>
										
										<asp:TemplateField HeaderText="Choose Category">
											<HeaderTemplate>
												<asp:CheckBox ID="chkHeader" runat="server" />
											</HeaderTemplate>
											<ItemTemplate>
												<%--<input type="checkbox" id="chkChild" />--%>
												<asp:CheckBox ID="chkChild" runat="server" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="FeesName" HeaderText="Fees Category Name">
											<HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
											<ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
										</asp:BoundField>
										<asp:BoundField DataField="FeesType" HeaderText="Fees Category Type">
											<HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
											<ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
										</asp:BoundField>
										<asp:TemplateField HeaderText="Amount In Rs.">
											<ItemTemplate>
												<asp:TextBox ID="txtFeesAmount" runat="server" Width="300px" CssClass="validate[custom[onlyNumberSp]] validate[required] TextBox" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;">0</asp:TextBox>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
									<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
									<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
									<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
									<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
								</asp:GridView>
							</div>

							<div style="text-align: right;" class="divclasswithoutfloat">
								<%-- &nbsp;&nbsp;--%>
								<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Detach" OnClick="btnSave_Click" />
								<%--&nbsp;&nbsp;--%>

								<%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue-new" OnClientClick="myFunction()" OnClick="btnCancel_Click" />--%>
							</div>
						</div>
					</div>
				</div>
				<div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
			</div>
		</div>
	</div>
	<script type="text/javascript">
	    jQuery("#aspnetForm").validationEngine('attach', {
	        promptPosition: "bottomRight",
	        validationEventTrigger: "submit",
	        validateNonVisibleFields: false,
	        updatePromptsPosition: true
	    });

	    function BindClass() {
	        //$(document).ready(function () {

	        bindClass();

	        // });
	    }

	    function bindClass() {
	        $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
	        //  var imgUrl = $(this).val();
	        //  $("#swapImg").attr("src", imgUrl);  
	        $.ajax({
	            type: "POST",
	            contentType: "application/json; charset=utf-8",
	            url: "Class_Template.aspx/LoadClass",
	            data: "{'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
		        dataType: "json",

		        success: function (data) {

		            // alert("Suceess");
		            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
		            $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

        		    //var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
        		    $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);

        		    var temp = $.parseJSON(data.d);
        		    var count = -1;
        		    $("#divLoading").show();
        		    $.each(temp, function (i) {
        		        //alert(i)
        		        //mkmok
        		        count = i;
        		        var optionhtml = '<option value="' +
                         temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
        		        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);
                        // alert(count);
                    });
        		    $("#divLoading").hide();
        		    if (count == "-1") {
        		        alert("Class is not Created");
        		    }


        		},
		        error: function (error) {
		            // alert("Error" + error);
		        }

		    });
        }
        $('[id*=chkHeader]').click(function () {

            if ($(this).is(":checked")) {

                $('[id*=chkChild]').prop("checked", true);
                //   $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkChild]').prop("checked", false);
                // $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });
        $("[id*=chkChild]").click(function () {
            if ($(this).is(":checked")) {
                //   $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            } else {
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    $('[id*=chkHeader]').prop("checked", true);
                    //   $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
			        $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
			    }
            }
            // alert("chkChild");
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                $('[id*=chkHeader]').prop("checked", true);
                //  $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
		        $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
		    }
		    else {
		        $('[id*=chkHeader]').removeAttr("checked");
		    }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                // $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
		        $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
		    }
        });

        $(document.getElementById('<%= ddlClass.ClientID %>')).change(function () {

	        $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
		    $.ajax({
		        type: "POST",
		        contentType: "application/json; charset=utf-8",
		        url: "Class_Template.aspx/LoadDivision",
		        data: "{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
        	    dataType: "json",
        	    success: function (data) {
        	        var count = -1;
        	        var temp = $.parseJSON(data.d);
        	        // alert("divLoadingShow");          
        	        $("#divLoading").show();
        	        $.each(temp, function (i) {
        	            count = i;
        	            var optionhtml = '<option value="' +
                         temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
        	            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);

            	    });
            	    $("#divLoading").hide();
            	    // alert("divLoadinghide");
            	    if (count == "-1") {
            	        alert("Division of this class is not Created");
            	    }

            	},
        	    error: function (error) {
        	        // alert("Error" + error);
        	    }

        	});
        });


	</script>
</asp:Content>