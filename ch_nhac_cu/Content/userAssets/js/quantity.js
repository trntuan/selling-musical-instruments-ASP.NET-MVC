document.addEventListener('DOMContentLoaded', function() {
  var btnMinus = document.querySelector('.btn-minus');
  var btnPlus = document.querySelector('.btn-plus');
  var input = document.querySelector('.quantity input');

  btnMinus.addEventListener('click', function() {
    var oldValue = parseInt(input.value);
    if (oldValue > 1) {
      var newValue = oldValue - 1;
      input.value = newValue;
    }
  });

  btnPlus.addEventListener('click', function() {
    var oldValue = parseInt(input.value);
    var newValue = oldValue + 1;
    input.value = newValue;
  });
});



