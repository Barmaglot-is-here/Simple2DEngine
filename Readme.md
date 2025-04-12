# Simple2DEngine
Простой 2Д движок на манер MonoGame.

В качестве графической подсистемы используется Direct2D, аудио - XAudio.

# Быстрый старт
Ниже приведён пример простого приложения.

**Programm.cs:**
```
TestApplication app = new();

app.Run();
```

**TestApplication.cs:**
```
﻿using Simple2DEngine;

public class TestApplication : Application {
  private readonly Sprite _sprite;

  public TestApplication()
  {
      Resources.Scan(*Путь до ресурсов приложения*);

      _sprite = Resources.Load<Sprite>("Sprite.png");
  }

  protected override void Draw()
  {      
      Renderer.DrawSprite(_sprite);
  }

  protected override void Update()
  {
      base.Update();

      if (Keyboard.Pressed(Key.Q))
          Window.Close();
  }
}
```

Разберём что здесь происходит.

```
  TestApplication app = new(); //Создаём экземпляр приложения

  app.Run(); //Запускаем
```

```
  public class TestApplication : Application //Любое приложение должно наследоваться от Application
                                             //Для доступа к циклу обновления/отрисовки
                                             //А так же базовым компонентам: окну, рендеру, звуковому движку
```

```
  private readonly Sprite _sprite; //Объявляем поле типа Sprite (Изображение)
                                   //Для дальнейшей загрузки в конструкторе
                                   //И последующей открисовки в метода Draw()
```

```
  public TestApplication()
  {
      Resources.Scan(*Путь до ресурсов приложения*); //Класс Resources ищет ресурсы приложения (картинки, музыку, текст и т.д.)
                                                     //В указанной папке и добавляет их во внутренний список

      _sprite = Resources.Load<Sprite>("Sprite.png"); //Загружаем картинку с именем "Sprite" через Resources
                                                      //Для загрузки ресурсов используются относительные пути.
                                                      //Расширение должно быть указано явно, иначе ресурс не будет найден
  }
```

```
  protected override void Draw() //В этом методе должна выполняться отрисовка графики посредством доступа к классу Render
  {      
      Renderer.DrawSprite(_sprite); //Получаем доступ к рендеру (это свойство класса Application, от которого мы унаследовали наше приложение)
                                    //И говорим ему нарисовать загруженную ранее картику
  }
```

```
  protected override void Update() //В этом классе должна выполняться игровая логика
  {
     if (Keyboard.Pressed(Key.Q)) //Проверяем, не нажата ли клавиша `Q` на клавиатуре
        Window.Close(); //Закрываем окно, если это так. (Window, равно как и Renderer, является свойством родительского класса Application)
  }
```

# Примеры
- [DrawImage](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/DrawImage)
- [DrawText](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/DrawText)
- [PlaySound](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/PlaySound)
- [ColorizeImage](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/ColorizeImage)
- [TransformImage](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/TransformImage)
- [Localization](https://github.com/Barmaglot-is-here/Simple2DEngine/tree/master/Samples/Localization)

# Используемые библиотеки
- Vortice: https://github.com/amerkoleci/Vortice.Windows
- Sharp.DX: https://github.com/sharpdx/SharpDX
- MP3Sharp: https://github.com/ZaneDubya/MP3Sharp
