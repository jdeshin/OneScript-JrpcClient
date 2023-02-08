# Клиент JRPC для OneScript

## Назначение

Библиотека позволяет прозрачно вызывать методы на удаленном сервере, в соответствии с спецификацией [JRPC 2.0](https://www.jsonrpc.org/specification) из OneScript. 

В качестве транспорта используется протокол http(s).

## Пример использования

```
// Пример вызова удаленной функции или процедуры

ПодключитьВнешнююКомпоненту("ПутьКБиблиотеке\OneScript-JrpcClient.dll");
КлиентJRPC = Новый КлиентJRPC();

// 
КлиентJRPC.АвтогенерацияId = Истина;
КлиентJRPC.Соединение = Новый HTTPСоединение("IPАдресСервераИлиDNSИмя");
КлиентJRPC.Запрос = Новый HTTPЗапрос("ОтносительныйURL");

Результат = КлиентJRPC.ФункцияНаУдаленномСервере(Аргумент1, , АргументN);
КлиентJRPC.ПроцедураНаУдаленномСервере(Аргумент1, , АргументN);
```

```
// Пример вызова notification

ПодключитьВнешнююКомпоненту("ПутьКБиблиотеке\OneScript-JrpcClient.dll");
КлиентJRPC = Новый КлиентJRPC();

// 
КлиентJRPC.АвтогенерацияId = Ложь;
КлиентJRPC.Соединение = Новый HTTPСоединение("IPАдресСервераИлиDNSИмя");
КлиентJRPC.Запрос = Новый HTTPЗапрос("ОтносительныйURL");
КлиентJRPC.Идентификатор = Неопределено;

КлиентJRPC.ПроцедураНаУдаленномСервере(Аргумент1, , АргументN);
```


## Описание

## Ограничения

Не реализованы batch requests
