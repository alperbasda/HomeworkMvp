<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="ListLessons.aspx.cs" Inherits="QuizMaker.WebUI.Views.Lesson.ListLessons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="Content" runat="server">

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
                            <div class="col-xl-4 order-1 order-xl-2 m--align-right">
                                <button type="button" class="btn btn-primary m-btn m-btn--custom m-btn--icon m-btn--air m-btn--pill" data-toggle="modal" data-target="#new-item">
                                    <span>
                                        <i class="la la-plus"></i>
                                        <span>Yeni Ders Ekle
                                        </span>
                                    </span>
                                </button>
                                <div class="m-separator m-separator--dashed d-xl-none"></div>
                            </div>
                        </div>
                    </div>
                    <!--end: Search Form -->
                    <!--begin: Datatable -->

                    <!--Table-->
                    <table class="table table-hover table-responsive-md table-fixed">
                        <div id="tableItems" runat="server">
                        </div>
                    </table>
                    <!--Table-->
                </div>
            </div>
            <!--end: Datatable -->
        </div>
        <!--begin::Islem Modal-->
        
            <div class="modal fade" id="delete-item" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title islem-title">Silme İşlemi
                            </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;
                                </span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="m-scrollable" data-scrollbar-shown="true" data-scrollable="true" data-height="200">
                                <form id="form1" runat="server">
                                    <div class="form-group lesson-name">
                                    </div>
                                    <asp:literal id="ltr" runat="server"></asp:literal>
                                    <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
                                    <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
                                </form>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary" id="confirm-delete-item" data-dismiss="modal">Sil</button>
                        </div>
                    </div>
                </div>
            </div>

        <!--end::Islem Modal-->

        <!--begin::Create Modal-->
        <div class="modal fade" id="new-item" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Yeni Ders
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;
                            </span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="m-scrollable" data-scrollbar-shown="true" data-scrollable="true" data-height="200">
                            <form>
                                <div class="form-group">
                                    <label for="lesson-name" class="form-control-label pull-left">
                                        Ders Adı
                                    </label>
                                    <input type="text" class="form-control" id="lesson-name" placeholder="Dersin Adını Giriniz...">
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">
                            Kapat
                        </button>
                        <button type="button" class="btn btn-primary" id="create-item" data-dismiss="modal">
                            Kaydet
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <!--end::Create Modal-->

    </div>





</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="../../scripts/LessonPageScripts.js"></script>
</asp:Content>
