# КлиентJRPC

## Описание

Библиотека позволяет прозрачно вызывать методы на удаленном сервере, в соответствии со спецификацией [JRPC 2.0](https://www.jsonrpc.org/specification) из OneScript. 

В качестве транспорта используется протокол http(s).

## Примеры использования

```pwsh
// Пример вызова удаленной функции или процедуры

ПодключитьВнешнююКомпоненту("ПутьКБиблиотеке\OneScript-JrpcClient.dll");
КлиентJRPC = Новый КлиентJRPC();

КлиентJRPC.АвтогенерацияId = Истина;
КлиентJRPC.Соединение = Новый HTTPСоединение("IPАдресСервераИлиDNSИмя");
КлиентJRPC.Запрос = Новый HTTPЗапрос("ОтносительныйURL");

Результат = КлиентJRPC.ФункцияНаУдаленномСервере(Аргумент1, , АргументN);
КлиентJRPC.ПроцедураНаУдаленномСервере(Аргумент1, , АргументN);
```

```pwsh
// Пример вызова notification

ПодключитьВнешнююКомпоненту("ПутьКБиблиотеке\OneScript-JrpcClient.dll");
КлиентJRPC = Новый КлиентJRPC();

КлиентJRPC.АвтогенерацияId = Ложь;
КлиентJRPC.Соединение = Новый HTTPСоединение("IPАдресСервераИлиDNSИмя");
КлиентJRPC.Запрос = Новый HTTPЗапрос("ОтносительныйURL");
КлиентJRPC.Идентификатор = Неопределено;

КлиентJRPC.ПроцедураНаУдаленномСервере(Аргумент1, , АргументN);
```


## Синтаксис

### Свойства

### Методы

## Замечания

Не реализованы batch requests
