<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="CreateExam.aspx.cs" Inherits="QuizMaker.WebUI.Views.Exam.CreateExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="m-portlet__body">

        <div class="card">
            <div class="card-body">
                <!--begin: Search Form -->
                <div class="m-form m-form--label-align-right m--margin-top-20 m--margin-bottom-30">
                    <div class="row align-items-center">
                        <div class="col-xl-8 order-2 order-xl-1">
                            <div class="form-group m-form__group row align-items-center">
                                <div class="col-md-3">
                                    <div class="m-input-icon m-input-icon--left">
                                        <input type="text" class="form-control m-input" placeholder="Arama..." id="Search">
                                        <span class="m-input-icon__icon m-input-icon__icon--left">
                                            <span>
                                                <i class="la la-search"></i>
                                            </span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!--end: Search Form -->
                <!--begin: Datatable -->



                <div id="tableItems" runat="server">
                    <!--Table-->


                    <!--Table-->
                </div>
            </div>
        </div>
        <!--end: Datatable -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="../../scripts/CreateExamJs.js"></script>
</asp:Content>
