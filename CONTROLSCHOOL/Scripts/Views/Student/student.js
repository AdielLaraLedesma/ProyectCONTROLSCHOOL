var AdminCourse = AdminCourse || {};

AdminCourse.Student = function () {
    var self = this;

    //self.student = null;
    self.course = null;
    self.teacher = null;
    self.coursestudent = null;

    self.controls = {

    };

    /*****************************************************/

    self.initControls = function () {
        self.controls.btnRegistry = document.querySelector("#btnRegister");
        self.controls.btnSearch = document.querySelector("#btnSearch");
        self.controls.modalEntity = document.querySelector("#modalEntity");
        self.controls.modalHeader = document.querySelector("#modalEntity h4.modal-title");
        self.controls.btnSave = document.querySelector("#btnSave");
        self.controls.btnCancel = document.querySelector("#btnCancel");

        self.controls.txtName = document.querySelector("#txtName1");
        self.controls.txtLastNames = document.querySelector("#txtLastNames");
        self.controls.txtAddress = document.querySelector("#txtAddress");
        self.controls.txtScore = document.querySelector("#txtScore");
        self.controls.txtSearch = document.querySelector("#txtSearch");
        self.controls.table = document.querySelector("table.table");

        self.controls.confirmation = AdminCourse.Confirmation();
    };

    self.initEvents = function () {
        self.controls.btnSearch.addEventListener("click", self.refreshTable);
        self.controls.btnRegistry.addEventListener("click", self.showModalRegistry);
        $(self.controls.modalEntity).on("hidden.bs.modal", self.clearFields);
        document.addEventListener("click", self.actionsClick);
    };

    self.initModals = function () {
        $(self.modalEntity).modal({
            backdrop: 'static',
            keyboard: false,
            show: false
        });
    };

    self.init = function () {
        self.initControls();
        self.initModals();
        self.initEvents();
        self.controls.btnCancel.style.display = "none";
    };

    self.entity = {};

    /*****************************************************/

    self.actionsClick = function (event) {
        if (event.target.classList.contains("edit")) {
            self.entity = $.extend({}, $(event.target.offsetParent).data("info"));
            self.student = self.entity.Student;
            self.showModalModify();
        }
        if (event.target.classList.contains("delete")) {
            self.entity = $.extend({}, $(event.target.offsetParent).data("info"));
            self.student = self.entity.Student;
            self.showModalDelete();
        }
    };

    self.clearFields = function () {
        self.controls.txtName.value = "";
        self.controls.txtLastNames.value = "";
        self.controls.txtAddress.value = "";
        self.controls.txtScore.value = "";

        self.controls.btnCancel.style.display = "none";
        self.student = null;
    };

    self.createButton = function (title, icon, event) {
        var btn = document.createElement("button");
        btn.classList.add("btn");
        btn.classList.add(event);
        btn.title = title;
        btn.type = "button";
        var glyph = document.createElement("span");
        glyph.setAttribute("aria-hidden", "true");
        glyph.classList.add("glyphicon");
        glyph.classList.add("glyphicon-" + icon);
        btn.appendChild(glyph);
        return btn;
    };

    self.refreshTable = function () {
        $.ajax({
            method: 'post',
            url: url.student.get,
            data: { text: self.controls.txtSearch.value },
            cache: false
        }).done(function (response) {
            if (response.IsError) {
                alert(response.Message);
            } else {
                var tbody = self.controls.table.querySelector("tbody");
                tbody.innerHTML = "";

                if (response.Result.length === 0) {
                    var tr = document.createElement("tr");
                    var td1 = document.createElement("td");
                    td1.textContent = "No hay datos registrados";
                    td1.colSpan = 6;
                    tr.appendChild(td1);
                    tbody.appendChild(tr);
                } else {

                    response.Result.forEach(function (r) {

                        var tr = document.createElement("tr");
                        var td1 = document.createElement("td"); td1.textContent = r.ID;
                        var td2 = document.createElement("td"); td2.textContent = r.Name;
                        var td3 = document.createElement("td"); td3.textContent = r.LastName;
                        var td4 = document.createElement("td"); td4.textContent = r.Address;
                        var td5 = document.createElement("td"); td5.textContent = r.Score;
                        var td6 = document.createElement("td");

                        var btnEdit = self.createButton("Editar", "pencil", "edit");
                        td6.appendChild(btnEdit);

                        var btnInactivate = self.createButton("Eliminar", "remove", "delete");
                        td6.appendChild(btnInactivate);

                        $(td6).data("info", r);


                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tr.appendChild(td4);
                        tr.appendChild(td5);
                        tr.appendChild(td6);

                        tbody.appendChild(tr);
                    });
                }
            }
        });
    };

    self.removeEventFromModal = function () {
        self.controls.btnSave.removeEventListener("click", self.register);
        self.controls.btnSave.removeEventListener("click", self.modify);
        self.controls.btnSave.removeEventListener("click", self.delete);
    };

    self.showModalRegistry = function () {
        self.controls.modalHeader.innerText = "Registrar Estudiante";
        self.controls.btnSave.innerText = "Registrar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.register);
        $(self.controls.modalEntity).modal("show");
    };

    self.register = function () {
        var info = {
            Name: self.controls.txtName.value,
            LastName: self.controls.txtLastNames.value,
            Address: self.controls.txtAddress.value,
            Score: self.controls.txtScore.value
        };

        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.student.register,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalModify = function () {
        self.controls.modalHeader.innerText = "Modificar Estudiante";
        self.controls.btnSave.innerText = "Guardar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.modify);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtLastNames.value = self.entity.LastName;
        self.controls.txtAddress.value = self.entity.Address;
        self.controls.txtScore.value = self.entity.Score;
        $(self.controls.modalEntity).modal("show");
    };

    self.modify = function () {
        var info = self.entity;
        info.Name = self.controls.txtName.value;
        info.LastName = self.controls.txtLastNames.value;
        info.Address = self.controls.txtAddress.value;
        info.Score = self.controls.txtScore.value;

        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.student.modify,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalDelete = function () {
        self.controls.modalHeader.innerText = "Eliminar Estudiante";
        self.controls.btnSave.innerText = "Eliminar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.delete);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtLastNames.value = self.entity.LastName;
        self.controls.txtAddress.value = self.entity.Address;
        self.controls.txtScore.value = self.entity.Score;
        self.controls.btnCancel.style.display = "";
        $(self.controls.modalEntity).modal("show");
    };

    self.delete = function () {
        var info = self.entity;
        info.IsActive = false;

        self.controls.btnSave.disabled = true;

        self.controls.confirmation.show("¿Desea eliminar el estudiante?", function () {
            $.ajax({
                method: 'post',
                url: url.student.delete,
                data: info,
                cache: false
            }).done(self.onSuccess);
        }, function () {
            self.onSuccess({ IsError: false });
        });

    };

    self.onSuccess = function (response) {
        if (response.IsError) {
            alert(response.Message);
        } else {
            $(self.controls.modalEntity).modal("hide");
            self.clearFields();
            self.controls.txtSearch.value = "";
            self.refreshTable();
        }
        self.controls.btnSave.disabled = false;
        self.refreshTable();
    };

   

    
   
    (function () {
        self.init();
        self.refreshTable();
    })();

    return {

    };
};


$(function () {
    var student = new AdminCourse.Student();
});