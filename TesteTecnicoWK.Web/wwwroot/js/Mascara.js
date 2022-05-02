function AddMascaraDinheiro(valor) {

    valor = String(valor).replace(".", ",");

    var posVirgula = String(valor).indexOf(',');
    var decimal = "";
    var mascara = "";

    if (posVirgula > 0) {
        decimal = valor.substring(posVirgula + 1, posVirgula + 3);
        mascara = 'R$ ' + valor.substring(0, posVirgula + 1);
    }
    else {
        decimal = ",00";
        mascara = 'R$ ' + valor;
    }

    if (decimal.length < 2) {
        decimal = decimal + "0";
    }

    mascara = mascara + decimal;
    return mascara
}

function RemoveMascaraDinheiro(valor) {
    var novoValor = valor;

    return novoValor.replace("R$", "").replace(" ", "").replace(".", "");
}