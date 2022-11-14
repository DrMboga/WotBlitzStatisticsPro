var themesHelper = themesHelper || {};

themesHelper.switchTheme = function (isDark) {
  var theme = isDark
    ? 'css/bootstrap/bootstrap-dark.min.css'
    : 'css/bootstrap/bootstrap.min.css';

  // Remove the previous theme from the head tag
  let head = document.getElementsByTagName('head')[0];
  head.querySelector('#theme').remove();

  // Add a new css link
  let newLink = document.createElement('link');
  newLink.setAttribute('id', 'theme');
  newLink.setAttribute('rel', 'stylesheet');
  newLink.setAttribute('type', 'text/css');
  newLink.setAttribute('href', theme);

  head.appendChild(newLink);
};
