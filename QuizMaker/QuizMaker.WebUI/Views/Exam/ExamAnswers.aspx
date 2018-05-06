<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="ExamAnswers.aspx.cs" Inherits="QuizMaker.WebUI.Views.Exam.ExamAnswers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="m-grid__item m-grid__item--fluid m-wrapper" style="margin-top: 3%;">
        <div class="m-portlet__body">
            <div class="card">
                <div class="card-body  text-center">
                    <form id="form1" runat="server">
                        <div id="tableItems" runat="server">
                            
                        </div>
                        <asp:Button ID="btnSubmit" runat="server" Text="Gönder" OnClick="btnSubmit_Click"></asp:Button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
