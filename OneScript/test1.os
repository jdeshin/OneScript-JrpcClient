
ПодключитьВнешнююКомпоненту("C:\Users\Administrator\Source\Repos\OneScript-JrpcClient\bin\Debug\OneScript-JrpcClient.dll");

КлиентJRPC = Новый КлиентJRPC();
КлиентJRPC.АвтогенерацияId = Истина;
КлиентJRPC.Соединение = Новый HTTPСоединение("192.168.1.89");
КлиентJRPC.Запрос = Новый HTTPЗапрос("/test-os/test.os);

Результат = КлиентJRPC.ТестоваяФункция(1);
//Док = Новый ТекстовыйДокумент();
//ТекстовыйДокумент.УстановитьТекст(Строка(Результат));
//ТекстовыйДокумент.Записать("C:\Users\Administrator\Source\Repos\OneScript-JrpcClient\OneScript\out.txt");
КлиентJRPC.ТестоваяПроцедура(1);