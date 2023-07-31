# Домашняя работа для пятого учебного семестра (3 год обучения, 1 семестр)

![.NET](https://github.com/<Вставьте свой ник github>/dotnet-homeworks-2/actions/workflows/dotnet.yml/badge.svg)
[![codecov](https://codecov.io/gh/max-arshinov/dotnet-homeworks-2/branch/master/graph/badge.svg?token={token})](https://codecov.io/gh/max-arshinov/dotnet-homeworks-2)

## Как устроены Actions
1. ***build***: *Проверка: собирается ли проект.*
2. ***test*** и ***test-report***: *Все тесты должны проходить*
4. ***codecov***: *Программа должна быть минимум на 80% покрыта тестами.* 
Тесты, которые уже были написаны заранее, проверяют работоспособность вашей программы:  верно ли выполнено задание.
Однако при реализации вы можете добавлять свои классы/сервисы и вот для них вы должны написать свои собственные тесты.
5. После этого в рамках локального репозитория создаётся пулл реквест из ветки с решённым домашним заданием в ветку master. Далее произойдёт автоматический запуск всех workflow:
- все workflow должны успешно отработать.
- если что-то не так, смотрите логи workflow, который не отработал и исправляете проблему.
- [как настроить codecov в репозиторий](https://docs.google.com/document/d/1DPAfO-v2acR-CmLviX3qCnTBwUYPyipARdPjUjTZKdo/edit?usp=sharing)
## Как выполнить домашку
1. В файле Dotnet.Homeworks.Tests.RunLogic/TestConfig.cs проставить номер текущей выполняемой домашки. Т.е. если выполняете домашку 1, то ставите HomeworkProgress(Homeworks.Docker) и т.д.
2. Открываете папку Theory и находите файл *.md по актуальной теме и знакомитесь с постановкой задачи. Выполняете поставленные требования в основном проекте Dotnet.Homeworks.MainProject.
3. Если пишете свои тесты, не забывайте поставить им кастомный xUnit-атрибут из Tests.RunLogic/Attributes по аналогии с уже написанными тестами. Если ваш тест - Theory - добавляете аттрибут [HomeworkTheory(Homeworks.{Актуальная тема})], если Fact - [Homework(Homeworks.{Актуальная тема})]. Подробнее о различиях можете почитать [здесь](https://codebots.com/docs/what-is-xunit)

- Для чего все эти атрибуты? - Чтобы тесты с домашками, которые вы ещё не выполнили, не влияли на прохождение github workflow, т.е. чтобы тесты следующих домашек не валились, пока вы до них не дойдёте.