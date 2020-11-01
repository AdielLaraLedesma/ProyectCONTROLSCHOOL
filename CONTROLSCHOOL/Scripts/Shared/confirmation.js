var AdminCourse = AdminCourse || {};

AdminCourse.Confirmation = function () {
    var self = this;

    self.controls = {

    };

    self.initControls = function () {
        self.controls.btnYes = document.querySelector("#btnYes");
        self.controls.btnNo = document.querySelector("#btnNo");
        self.controls.modal = document.querySelector("#modalConfirmation");
        self.controls.modalMessage = document.querySelector("#confirmation_message");
    };

    self.initEvents = function () {
        self.controls.btnYes.addEventListener("click", self.onYes);
        self.controls.btnNo.addEventListener("click", self.onNo);
    };

    self.initModals = function () {
        $(self.modal).modal({
            backdrop: 'static',
            keyboard: false,
            show: false
        });
    };

    self.init = function () {
        self.initControls();
        self.initModals();
        self.initEvents();
    };

    self.onYes = function () {
        $(self.controls.modal).modal("hide");
        self.yes();
    };

    self.onNo = function () {
        $(self.controls.modal).modal("hide");
        self.no();
    };

    self.show = function (title, onYes, onNo) {
        self.controls.modalMessage.innerText = title;
        self.yes = onYes;
        self.no = onNo;
        $(self.controls.modal).modal("show");
    };

    (function () {
        self.init();
    })();

    return {
        show: self.show
    };
};