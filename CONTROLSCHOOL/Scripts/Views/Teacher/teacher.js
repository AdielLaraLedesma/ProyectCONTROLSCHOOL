var AdminCourse = AdminCourse || {};

AdminCourse.Teacher = function () {
    var self = this;

    //self.teacher = null;
    self.course = null;
    self.student = null;
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
        self.controls.txtWage = document.querySelector("#txtWage");
        self.controls.selectHasPlaza = document.querySelector("#HasPlaza");
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
            self.teacher = self.entity.Teacher;
            self.showModalModify();
        }
        if (event.target.classList.contains("delete")) {
            self.entity = $.extend({}, $(event.target.offsetParent).data("info"));
            self.teacher = self.entity.Teacher;
            self.showModalDelete();
        }
    };

    self.clearFields = function () {
        self.controls.txtName.value = "";
        self.controls.txtLastNames.value = "";
        self.controls.txtAddress.value = "";
        self.controls.txtWage.value = "";
        self.controls.btnCancel.style.display = "none";
        self.teacher = null;
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
            url: url.teacher.get,
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
                    td1.colSpan = 7;
                    tr.appendChild(td1);
                    tbody.appendChild(tr);
                } else {

                    response.Result.forEach(function (r) {

                        var tr = document.createElement("tr");
                        var td1 = document.createElement("td"); td1.textContent = r.ID;
                        var td2 = document.createElement("td"); td2.textContent = r.Name;
                        var td3 = document.createElement("td"); td3.textContent = r.LastName;
                        var td4 = document.createElement("td"); td4.textContent = r.Address;
                        var td5 = document.createElement("td"); td5.textContent = r.Wage;
                        var td6 = document.createElement("td"); r.HasPlaza ? td6.textContent = "Si" : td6.textContent = "No";
                        var td7 = document.createElement("td");

                        var btnEdit = self.createButton("Editar", "pencil", "edit");
                        td7.appendChild(btnEdit);

                        var btnInactivate = self.createButton("Eliminar", "remove", "delete");
                        td7.appendChild(btnInactivate);

                        $(td7).data("info", r);


                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tr.appendChild(td4);
                        tr.appendChild(td5);
                        tr.appendChild(td6);
                        tr.appendChild(td7);

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
        self.controls.modalHeader.innerText = "Registrar Profesor";
        self.controls.btnSave.innerText = "Registrar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.register);
        $(self.controls.modalEntity).modal("show");
    };

    self.register = function () {
        var plaza = self.controls.selectHasPlaza.options[self.controls.selectHasPlaza.selectedIndex].value;
        if (plaza == "Si") {
            plaza = true;
        } else {
            plaza = false;
        }
        var info = {
            Name: self.controls.txtName.value,
            LastName: self.controls.txtLastNames.value,
            Address: self.controls.txtAddress.value,
            Wage: self.controls.txtWage.value,
            HasPlaza: plaza
        };

        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.teacher.register,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalModify = function () {
        self.controls.modalHeader.innerText = "Modificar Profesor";
        self.controls.btnSave.innerText = "Guardar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.modify);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtLastNames.value = self.entity.LastName;
        self.controls.txtAddress.value = self.entity.Address;
        self.controls.txtWage.value = self.entity.Wage;
        $(self.controls.modalEntity).modal("show");
    };

    self.modify = function () {
        var plaza = self.controls.selectHasPlaza.options[self.controls.selectHasPlaza.selectedIndex].value;
        if (plaza == "Si") {
            plaza = true;
        } else {
            plaza = false;
        }
        var info = self.entity;
        info.Name = self.controls.txtName.value;
        info.LastName = self.controls.txtLastNames.value;
        info.Address = self.controls.txtAddress.value;
        info.Wage = self.controls.txtWage.value;
        info.HasPlaza = plaza;

        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.teacher.modify,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalDelete = function () {
        self.controls.modalHeader.innerText = "Eliminar Profesor";
        self.controls.btnSave.innerText = "Eliminar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.delete);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtLastNames.value = self.entity.LastName;
        self.controls.txtAddress.value = self.entity.Address;
        self.controls.txtWage.value = self.entity.Wage;
        //self.controls.txtHasPlaza.value = self.entity.HasPlaza;
        //self.controls.selectHasPlaza = self.entity.HasPlaza;
        self.controls.btnCancel.style.display = "";
        $(self.controls.modalEntity).modal("show");
    };

    self.delete = function () {
        var info = self.entity;
        info.IsActive = false;

        self.controls.btnSave.disabled = true;

        self.controls.confirmation.show("¿Desea eliminar el Profesor?", function () {
            $.ajax({
                method: 'post',
                url: url.teacher.delete,
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
    var teacher = new AdminCourse.Teacher();
});