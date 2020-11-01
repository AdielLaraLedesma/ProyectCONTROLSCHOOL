var AdminCourse = AdminCourse || {};

AdminCourse.Course = function () {
    var self = this;

    //self.course = null;
    self.student = null;
    self.teacher = null;
    self.coursetudent = null;

    self.controls = {

    };

    /*****************************************************/

    self.initControls = function () {
        self.controls.btnRegistry = document.querySelector("#btnRegister");
        self.controls.btnSearch = document.querySelector("#btnSearch");
        self.controls.modalEntity = document.querySelector("#modalEntity");
        self.controls.modalHeader = document.querySelector("#modalEntity h4.modal-title");

        self.controls.mbody = document.querySelector("#modalEntity .modal-dialog #modal-body .form-horizontal");

        self.controls.btnSave = document.querySelector("#btnSave");
        self.controls.btnCancel = document.querySelector("#btnCancel");

        self.controls.txtName = document.querySelector("#txtName1");
        self.controls.txtGrade = document.querySelector("#txtGrade");

        self.controls.txtSearch = document.querySelector("#txtSearch");
        self.controls.table = document.querySelector("table.table");

        self.controls.confirmation = AdminCourse.Confirmation();
    };

    self.initEvents = function () {
        //self.controls.txtIDTeacher.addEventListener("keyup", self.matchTeacher);


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
            self.course = self.entity.Course;
            console.log("epis");
            self.showModalModify();
        }
        if (event.target.classList.contains("delete")) {
            self.entity = $.extend({}, $(event.target.offsetParent).data("info"));
            self.course = self.entity.Course;
            self.showModalDelete();
        }
        if (event.target.classList.contains("IDTeacher")) {
            self.matchTeacher(event.target.options[event.target.selectedIndex].value);
        }

    };

    self.clearFields = function () {
        self.controls.txtName.value = "";
        self.controls.txtGrade.value = "";
        //self.controls.txtIDTeacher.value = "";

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

    self.refreshModal = function () {
        $.ajax({
            method: 'post',
            url: url.teacher.get,
            data: { text: self.controls.txtSearch.value },
            cache: false
        }).done(function (response) {
            
            if (!response.IsError) {
                var div = document.createElement("div");
                div.classList.add("form-group");
                var label = document.createElement("label");
                label.classList.add("control-label");
                label.classList.add("col-md-3");
                label.innerHTML = "ID Profesor";
                var div2 = document.createElement("div");
                div2.classList.add("col-md-9");
                var select = document.createElement("select");
                select.classList.add("IDTeacher");
                if (response.Result.length === 0) {
                    select.disabled = true;
                } else {
                    var i = 0;
                    response.Result.forEach(function (r) {
                        if (i == 0) {
                            self.matchTeacher(r.ID);
                        }
                        i++;
                        var option = document.createElement("option");
                        option.classList.add(r.ID);
                        option.innerHTML = r.ID;
                        select.appendChild(option);
                    });
                }
                    div2.appendChild(select);
                    div.appendChild(label);
                    div.appendChild(div2);
                    self.controls.mbody.appendChild(div);
            }
        });
    };



    self.refreshTable = function () {
        console.log(self.controls.txtSearch.value);
        $.ajax({
            method: 'post',
            url: url.course.get,
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
                        var td3 = document.createElement("td"); td3.textContent = r.Grade;
                        var td4 = document.createElement("td"); td4.textContent = r.Teacher.Name;
                        var td5 = document.createElement("td");

                        var btnEdit = self.createButton("Editar", "pencil", "edit");
                        td5.appendChild(btnEdit);

                        var btnInactivate = self.createButton("Eliminar", "remove", "delete");
                        td5.appendChild(btnInactivate);

                        $(td5).data("info", r);


                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tr.appendChild(td4);
                        tr.appendChild(td5);

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
        self.controls.modalHeader.innerText = "Registrar Curso";
        self.controls.btnSave.innerText = "Registrar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.register);
        $(self.controls.modalEntity).modal("show");
    };

    self.register = function () {
        //var e = document.querySelector(".IDTeacher");
        //self.matchTeacher(e.options[e.selectedIndex].value);
        var info = {
            Name: self.controls.txtName.value,
            Grade: self.controls.txtGrade.value,
            Teacher: self.teacher
        };
        
        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.course.register,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalModify = function () {
        self.controls.modalHeader.innerText = "Modificar Curso";
        self.controls.btnSave.innerText = "Guardar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.modify);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtGrade.value = self.entity.Grade;
        //self.controls.txtIDTeacher.value = self.entity.IDTeacher;
        //self.controls.txtIDTeacher.value = self.entity.Teacher.ID;
        $(self.controls.modalEntity).modal("show");
    };

    self.modify = function () {
        var info = self.entity;
        info.Name = self.controls.txtName.value;
        info.Grade = self.controls.txtGrade.value;
        //info.IDTeacher = self.controls.txtIDTeacher.value;
        info.Teacher = self.teacher;

        self.controls.btnSave.disabled = true;

        $.ajax({
            method: 'post',
            url: url.course.modify,
            data: info,
            cache: false
        }).done(self.onSuccess);
    };

    self.showModalDelete = function () {
        self.controls.modalHeader.innerText = "Eliminar Curso";
        self.controls.btnSave.innerText = "Eliminar";
        self.removeEventFromModal();
        self.controls.btnSave.addEventListener("click", self.delete);
        self.controls.txtName.value = self.entity.Name;
        self.controls.txtGrade.value = self.entity.Grade;
        document.querySelector(".IDTeacher").disabled = true;
        //self.controls.txtIDTeacher.value = self.entity.IDTeacher;
        //self.controls.txtIDTeacher.value = self.entity.Teacher.ID;

        self.controls.btnCancel.style.display = "";
        $(self.controls.modalEntity).modal("show");
    };

    self.delete = function () {
        var info = self.entity;

        self.controls.btnSave.disabled = true;

        self.controls.confirmation.show("¿Desea eliminar el curso?", function () {
            $.ajax({
                method: 'post',
                url: url.course.delete,
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
            //self.refreshModal();
        }
        self.controls.btnSave.disabled = false;
        document.querySelector(".IDTeacher").disabled = true;
    };



   

    self.matchTeacher = function (id) {
        self.controls.btnSave.disabled = true;
        $.ajax({
            method: 'post',
            url: url.teacher.getbyid,
            data: {id},
            cache: false
        }).done(function (response) {
            if (response.IsError) {
                alert(response.Message);
            } else {
                self.teacher = response.Result;
                if (!self.teacher) {
                    alert("No existe un maestro registrado que coincida con el ID");
                } else {
                    self.controls.btnSave.disabled = false;
                }
                
            }
        });
    };



    (function () {
        self.init();
        self.refreshTable();


        self.refreshModal();
    })();

    return {

    };
};


$(function () {
    var course = new AdminCourse.Course();
});