function Exibir_Toast(tipo, mensagem) {
    $("#toast").removeClass("bg-danger");
    $("#toast").removeClass("bg-success");

    if (tipo == "sucesso") {
        $("#toast").addClass("bg-success");
    }

    if (tipo == "erro") {
        $("#toast").addClass("bg-danger");
    }

    if (tipo == "informacao") {
        $("#toast").addClass("bg-primary");
    }

    $("#toast-message").html(mensagem);

    var toast = new bootstrap.Toast($("#toast"));
    toast.show();
};