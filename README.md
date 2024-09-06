<H1>RabbitMQ</H1>
<h3><i>Introductions :</i></h3>
<p>در تعریفی ساده ربیت ام کیو یک سیستم پیان رسان می باشد که امکان انتقال پیام و ارتباط گیری بین سرویس ها را به شیوه های مختلف فراهم می کند.</p>

<h3><i>Process :</i></h3>
<img src="https://i.postimg.cc/bwMf5x5k/image-default.webp">

<h3><i>Entities :</i></h3>
<ul>
  <li><b>Producer</b> : سرویس تولید کننده پیام می باشد</li>
  <blockquote>routing key کلید ارسال پیام به exchange می باشد که برابر بودن آن با نام صف یا کلید ارتباطی صف ، مشخص می کند پیغام به کدام صف ارسال شود.
  <br>چناچه در زمان ارسال پیغام ، اشاره به exchange خاصی نشود ، حالت پیشفرض در نظر گرفته شده و باید routing key برابر نام صف مقصد باشد.
  <br>پیام بصورت باینری ارسال و دریافت می شود</blockquote>
  <li><b>Exchange</b> : در دنیای واقعی میتوان به اداره پست اشاره کرد در واقع وظیفه آن دریافت پیغام از تولید کننده پیام و ارسال آن به صف ها می باشد، چناچه صف خاصی به آن متصل نباشد میتواند در خصوص پیامی که دریافت کرده است تصمیم گیری کند ( آن را به ارسال کننده بازگرداند یا ... </li>
  <blockquote><h5>Attributes :
  <ul>
    <li>name : نام exhchange</li>
    <li>exchange yype :Defult , Direct , Fanout , Topic , Header </li>
    <li>durable : زمانی که سیستم reset می شود exchange نگه داری شود یا خیر ؟</li>
    <li>auto delete : اگر صف یا producer به exchange متصل نشده بود یا بلااستفاده بود اتوماتیک حذف شود یا خیر ؟</li>
    <li>arguments : آرگومان های ورودی</li>
  </ul>
  </h5></blockquote>
  <li><b>Queue</b> : صف ها، وظیفه دریافت پیام از exchange  و رساندن آن به دریافت کننده پیام را دارند.</li>
  <blockquote>binding key کلید ارتباط میان صف و exchange می باشد که در حالت های direct و topic بسیار مهم می باشد.
  <br>این کلید میتواند یک کلمه یا یک عبارت الگویی باشد که در ادامه با مثال بیشتر متوجه خواهد شد.
  </blockquote>
    <blockquote><h5>Attributes :
  <ul>
    <li>name : نام صف</li>
    <li>durable : زمانی که سیستم ریست می شود صف نگه داری شود یا خیر ؟</li>
    <li>exclusive : زمانی که اتصال صف قطع شد بصورت اتوماتیک حذف شود ( استفاده برای صف های موفتی )</li>
    <li>auto delete : اگر صف بلا استفاده بود یا پیامی نداشت یا به استفاده کننده ای متصل نبود بصورت اتوماتیک حذف شود یا خیر ؟</li>
    <li>arguments : آرگومان های ورودی</li>
  </ul>
  </h5></blockquote>
  <li><b>Consumer</b> : استفاده کننده پیام می باشد</li>
</ul>

<h3><i>Echange Types:</i></h3>
<uL>
  <li><b>Defult Exchange :</b>در این نوع ارسال route key یا کلید ارتباطی نام صف می باشد</li>
  <li><b>Direct Exchange :</b>در این نوع ارتباط زمانی پیام به صفی که bind key آن با route key پیام یکی باشد ارسال خواهد شد.</li>
  <img src="https://i.postimg.cc/ZnhKsCHP/Direct-Exchange1.png">
  <li><b>Topic Exchange :</b>همانند Direct exchange می باشد با این تفاوت که binding key یک الگو می باشد و در این الگو * یا ستاره به معنای هر چیزی و # به معنای یک یا هر تعداد عبارت می باشد</li>
  <img src="https://i.postimg.cc/xdrGRPHc/Topic-Exchange2.png">
  <li><b>Fanout Exchange :</b> در این نوع ارتباط پیام به کلیه صف های متصل ارسال خواهد شد. به همین دلیل routing key را null قرار می دهیم :</li>
  <img src="https://i.postimg.cc/pLjzpn7x/Fanout-Exchange2.png">
</uL>

<h3><i>Links :</i></h3>
<b>RabbitMQ website : <a href="https://rabbitmq.com">official website</a></b>
<br>
<b>RabbitMQ Simulator : <a href="https://tryrabbitmq.com">Try RabbitMQ</a></b>




