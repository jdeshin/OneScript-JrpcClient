﻿Сообщить("AA");

ПодключитьВнешнююКомпоненту("C:\Users\Administrator\Source\Repos\OneScript-JrpcClient\bin\Debug\OneScript-JrpcClient.dll");

КлиентJRPC1 = Новый КлиентJRPC();
Сообщить(Строка(КлиентJRPC1.АвтогенерацияId));
Сообщить(Строка(КлиентJRPC1.Соединение = Неопределено));
Сообщить(Строка(КлиентJRPC1.Запрос = Неопределено));
Сообщить(Строка(КлиентJRPC1.Идентификатор = Неопределено));
Сообщить(Строка(КлиентJRPC1.НастройкиСериализацииJSON = Неопределено));
Сообщить(Строка(КлиентJRPC1.ФункцияПреобразованияЗаписи = Неопределено));
КлиентJRPC1.АвтогенерацияId = Ложь;
КлиентJRPC1.Соединение = Новый HTTPСоединение("192.168.1.89");
КлиентJRPC1.Запрос = Новый HTTPЗапрос("/test-os/test.os");
КлиентJRPC1.Идентификатор = Неопределено;

Результат1 = КлиентJRPC1.ТестоваяФункция(2,2,2);
Сообщить(Строка(Результат1));
//Док = Новый ТекстовыйДокумент;
//ТекстовыйДокумент.УстановитьТекст(Строка(Результат));
//ТекстовыйДокумент.Записать("C:\Users\Administrator\Source\Repos\OneScript-JrpcClient\OneScript\out.txt");
//КлиентJRPC1.ТестоваяПроцедура(1);