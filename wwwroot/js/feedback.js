function ProcessForm(){
    // Инициализация объекта формы
    let data = new FormData();
   
    // Вытаскиваем информацию из полей на странице и добавляем в объект формы
    data.set("From", document.querySelector('[name="From"]').value)
    data.set("Text", document.querySelector('[name="Text"]').value)
   
    // создаем объект запроса
    let postRequest = new XMLHttpRequest();
    postRequest.open('POST', document.URL, true ); /* true = запрос будет выполнен асинхронно */
   
    // посылаем запрос
    postRequest.send(data);
   
     // Функция - обработчик результата с сервера
     postRequest.onload =  function () {
         // сохраняем ответ сервера. не лишним будет также добавить проверку на успешный запрос
         let serverMessage = this.response;
        
        /* Код ниже изменяет значение формы */
       
        // Находим контейнер
        let element = document.getElementsByClassName("container")[1];
        // Удаляем вложенный контейнер с формой
        element.children[0].remove();
       
        // Создаем новый элемент
        let paragraph = document.createElement("h5");
        // Добавляем ему стиль
        paragraph.style = "text-align : center"
        // Сохраняем в него сообщение сервера
        paragraph.innerText = serverMessage;
       
        // Добавляем дочерные элементы на страницу
        element.appendChild(document.createElement("br"))
        element.appendChild(paragraph)
    };
 }