$.validator.setDefaults({
    ignore: ":hidden:not(:visible.k-widget .kendo-force-validation)",
    highlight: function (element, errorClass, validClass) {
        if (element.type === "radio") {
            this.findByName(element.name).addClass(errorClass).removeClass(validClass);
        } else {
            $(element).addClass(errorClass).removeClass(validClass);
            if ($(element).hasClass("kendo-force-validation")) {
                $(element).closest(".k-widget").addClass('kendo-validation-error');
            }
        }
    },
    unhighlight: function (element, errorClass, validClass) {
        if (element.type === "radio") {
            this.findByName(element.name).removeClass(errorClass).addClass(validClass);
        } else {
            $(element).removeClass(errorClass).addClass(validClass);
            if ($(element).hasClass("kendo-force-validation")) {
                $(element).closest(".k-widget").removeClass('kendo-validation-error');
            }
        }
    }
});
