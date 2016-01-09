Чемпионат
Любое количество пар участвует в чемпионате  
Соревнования 3 вида: квалификация, короткая, произвольная программа.
Соревнования оценивается по 6-бальной шкале.
Оценивают 9 судей
Записывают все это в протоколы.
Существует итоговый протокол.

1. Значит у нас будут следующие классы:
* Сhampionship (Чемпионат)
* Participant (Участник соревнований)
* PairParticipant (Пара, состоящая из участников)
* Сompetition (Соревнование)
* Judge (Судья)
* JudgeProtocol (Судейский протокол)
Начнем с простых классов:
1. Private class Participant (Участник соревнований) содержит поля: 
    * name: string
    * surname: string

2. Public class PairParticipant (Пара, состоящая из участников) содержит поля: 
    * participantFirst: Participant
    * participantSecond: Participant

3. public class Сompetition (Соревнование) содержит поля: 
    * name: String
    * score: Integer

4. public class Judge (Судья) содержит поля: 
    * name: string
    * surname: string

5. public class JudgeProtocol (Судейский протокол) содержит поля: 
    * judge: Judge
    * pair: PairParticipant
    * competition: Competition
    * score: integer

6. Public class Сhampionship (Чемпионат) содержит поля:
    * pairsParticipant: список (не массив) типа PairParticipant
    * judges:  массив типа Judge
    * competitions: массив типа Judge
    * judgesProtocol: список (не массив) типа JudgeProtocol
 
