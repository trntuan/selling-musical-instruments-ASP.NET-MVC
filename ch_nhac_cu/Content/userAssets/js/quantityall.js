
document.addEventListener('DOMContentLoaded', function () {
    var btnMinusList = document.querySelectorAll('.btn-minus');
    var btnPlusList = document.querySelectorAll('.btn-plus');
    var quantityInputs = document.querySelectorAll('.quantity input');

    btnMinusList.forEach(function (btnMinus, index) {
        btnMinus.addEventListener('click', function () {
            var input = quantityInputs[index];
            var oldValue = parseInt(input.value);
            if (oldValue > 1) {
                var newValue = oldValue - 1;
                input.value = newValue;
                updatePrice(input);
                updateTotalValue();
            }
        });
    });

    btnPlusList.forEach(function (btnPlus, index) {
        btnPlus.addEventListener('click', function () {
            var input = quantityInputs[index];
            var oldValue = parseInt(input.value);
            var newValue = oldValue + 1;
            input.value = newValue;
            updatePrice(input);
            updateTotalValue();
        });
    });

    function updatePrice(input) {
        var row = input.closest('tr');
        var priceCell = row.querySelector('.price-value');
        var unitPrice = parseFloat(row.querySelector('.product-unit-price').textContent);
        var quantity = parseInt(input.value);
        var totalPrice = unitPrice * quantity;

        priceCell.textContent = totalPrice;
    }

    function updateTotalValue() {
        var prices = document.querySelectorAll('.price-value');
        var totalValue = 0;

        prices.forEach(function (price) {
            var priceValue = parseFloat(price.textContent);
            totalValue += priceValue;
        });

        var totalValueElement = document.querySelector('#total-value');
        totalValueElement.textContent = totalValue;
    }
});
