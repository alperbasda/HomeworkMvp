<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="ListQuestions.aspx.cs" Inherits="QuizMaker.WebUI.Views.Question.ListQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Contents/Style/QuestionStyle.css" rel="stylesheet" />
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
                                    <div class="m-form__group m-form__group--inline">
                                        <div class="form-group m-form__group">
                                            <div class="m-form__control">
                                                <select class="form-control" id="LessonSelector" runat="server">
                                                    <option value="-1" selected>Ders Seçin</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-md-none m--margin-bottom-10"></div>
                                </div>
                                <div class="col-md-3">
                                    <div class="m-form__group m-form__group--inline">
                                        <div class="m-form__control">
                                            <select class="form-control" id="TypeSelector" runat="server">
                                                <option value="-1" selected>Soru Tipi Seçin</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="d-md-none m--margin-bottom-10"></div>
                                </div>
                                <div class="col-md-3">
                                    <div class="m-form__group m-form__group--inline">
                                        <div class="form-group m-form__group">
                                            <div class="m-form__control">
                                                <select class="form-control" id="DifficultySelector" runat="server">
                                                    <option value="-1" selected>Zorluk Seçin</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-md-none m--margin-bottom-10"></div>
                                </div>
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
                        <div class="col-xl-4 order-1 order-xl-2 m--align-right">
                            <div class="btn-group">
                                <button class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" type="button" data-toggle="modal" data-target="#new-item" id="new-question">
                                    <span>
                                        <i class="la la-plus"></i>
                                        <span>Yeni Soru Ekle
                                        </span>
                                    </span>
                                </button>
                                <button class="btn btn-success m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" type="button" data-target="#new-item" id="oto-exam">
                                    <span>
                                        <span>Otm. Sınav Oluştur
                                        </span>
                                        &nbsp
                                        <i class="la la-plus"></i>
                                    </span>
                                </button>
                            </div>
                            <div class="m-separator m-separator--dashed d-xl-none"></div>
                        </div>
                    </div>
                </div>
                <!--end: Search Form -->
                <!--begin: Datatable -->



                <div id="tableItems" runat="server">
                    <!--Table-->
                    <table class="table table-hover table-responsive-md table-fixed">
                    </table>
                    <!--Table-->
                </div>


            </div>
        </div>
        <!--end: Datatable -->
    </div>





    <!--begin::İşlem Modal-->

    <div class="modal fade" id="new-item" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <form id="form1" runat="server">
                    <div id="change-dialog">
                        <div class="modal-header">
                            <h5 class="modal-title islem-title">Yeni Soru
                            </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;
                                </span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="m-scrollable question-items" data-scrollbar-shown="true" data-scrollable="true" data-height="200">
                                <div class="form-group">
                                    <div class="row">
                                        <label for="selectLesson" class="col-lg-2">Ders : </label>
                                        <select name="selectLesson" class="form-control col-lg-9" id="selectLesson" runat="server">
                                            <option value="" selected>Ders Seçin</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="selectType" class="col-lg-2">Tip : </label>
                                        <select name="selectType" class="form-control col-lg-9" id="selectType" onchange="ChangeAnswerItem()" runat="server">
                                            <option value="" selected>Soru Tipi Seçin</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="selectDifficulty" class="col-lg-2">Zorluk : </label>
                                        <select name="selectDifficulty" class="form-control col-lg-9" id="selectDifficulty" runat="server">
                                            <option value="" selected>Zorluk Seçin</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="points" class="col-lg-2">Puan : </label>
                                        <input name="points" id="points" type="number" class="form-control col-lg-9" value="10" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <textarea name="question" class="form-control" id="question" placeholder="Sorunuzu yazın"></textarea>
                                </div>
                                <div id="answer-items">
                                </div>
                            </div>
                            <asp:Literal ID="ltr" runat="server"></asp:Literal>
                            <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
                            <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                Kapat
                            </button>
                            <button type="button" class="btn btn-primary" id="create-item" onclick="CreateItem()" data-dismiss="modal">
                                Kaydet
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!--end::İşlem Modal-->
    <input type="hidden" id="otoexam" name="otoexam" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="../../scripts/QuestionPageScript.js"></script>
</asp:Content>
