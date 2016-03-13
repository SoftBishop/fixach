using System;
using System.Collections.Generic;
using System.Linq;

namespace fixach
{
    public class Participant
    {

        public string name;
        public string surname;
        public bool sex; // true - man, false - woman
        public Participant(string name, string surname, bool sex)
        {
            this.name = name;
            this.surname = surname;
            this.sex = sex;
        }

        private string formatSexToString()
        {
            if (sex == true)
                return "man";
            else
                return "woman";
        }

        public void display()
        {
            string sexToString = formatSexToString();
            System.Console.Write(name + " " + surname + " " +sexToString);
        }
    }

    public class PairParticipant
    {
        public string name;
        public Participant participantFirst;
        public Participant participantSecond;

        public PairParticipant(string namePair, Participant participantFirst, Participant participantSecond)
        {
            this.name = namePair;
            this.participantFirst = participantFirst;
            this.participantSecond = participantSecond;
        }
        public PairParticipant(string namePair,
            string nameParticipantFirst, string surnameParticipantFirst, bool sexParticipantFirst,
            string nameParticipantSecond, string surnameParticipantSecond, bool sexParticipantSecond)
        {
            this.name = namePair;
            this.participantFirst = new Participant(nameParticipantFirst, surnameParticipantFirst, sexParticipantFirst);
            this.participantSecond = new Participant(nameParticipantSecond, surnameParticipantSecond, sexParticipantSecond);
        }
        public void display()
        {
            System.Console.WriteLine(name);
            System.Console.Write("|_ "); participantFirst.display(); System.Console.Write("\n");
            System.Console.Write("|_ "); participantSecond.display(); System.Console.Write("\n");
        }
    }

    public class Сompetition
    {
        public string name;
        public int score;
        public double coefficient;
        public Сompetition(string name, int score, double coefficient)
        {
            if (score <= 0) throw new Exception("Score must be greater than 0");
            if (coefficient < 0 ) throw new Exception("Coefficient must be greater than 0");
            this.name = name;
            this.score = score;
            this.coefficient = coefficient;
        }
        public void display()
        {
            System.Console.Write("Competition " + name + " " + "score is " + score.ToString() + ", coeff " + coefficient.ToString());
        }
    }

    public class Judge
    {
        public string name;
        public string surname;
        public bool sex; // true - man, false - woman
        public Judge(string name, string surname, bool sex)
        {
            this.name = name;
            this.surname = surname;
            this.sex = sex;
        }

        private string formatSexToString()
        {
            if (sex == true)
                return "man";
            else
                return "woman";
        }

        public void display()
        {
            string sexToString = formatSexToString();
            System.Console.Write(name + " " + surname + " " + sexToString);
        }
    }

    public class JudgeProtocol
    {
        public Judge judge;
        public PairParticipant pair;
        public Сompetition competition;
        public int score;

        public JudgeProtocol(Judge judge, PairParticipant pair, Сompetition competition, int score)
        {
            this.judge = judge;
            this.pair = pair;
            this.competition = competition;

            if (score > competition.score && score < 0)
                throw new Exception("Judge score must be less or equal competition score and must be greater 0");
            else
                this.score = score;
        }

        public void display()
        {
            judge.display(); System.Console.Write("scores pair: " + score.ToString()  + " --- "); competition.display(); System.Console.Write("\n");
            pair.display(); 
        }
    }

    public class Сhampionship
    {
        private static int JUDGE_NUMBER = 9;
        private static int COMPETITION_NUMBER = 3;

        List<PairParticipant> pairs = new List<PairParticipant>();
        List<Judge> judges = new List<Judge>();
        List<Сompetition> competitions = new List<Сompetition>();
        List<JudgeProtocol> protocols = new List<JudgeProtocol>();

        public void addPair(PairParticipant pair)
        {
            pairs.Add(pair);
        }

        public void addJudge(Judge judge)
        {
            if (judges.Count > JUDGE_NUMBER)
                throw new Exception("Judge score must be less or equal competition score and must be greater 0");
            else
            {
                judges.Add(judge);
            }
        }

        public void addCompetition(Сompetition competition)
        {
            if (competitions.Count > COMPETITION_NUMBER)
                throw new Exception("Judge score must be less or equal competition score and must be greater 0");
            else
            {
                competitions.Add(competition);
            }
        }

        public void addProtocol(JudgeProtocol protocol)
        {
            protocols.Add(protocol);
        }

        public void displayWinnerInCompetitions()
        {
            foreach (Сompetition competition in competitions)
            {
                var CompetitionProtocols =  this.protocols.FindAll(protocol => protocol.competition == competition);
                var pairsInCompetition   = CompetitionProtocols.GroupBy(protocol => protocol.pair);
                var data = pairsInCompetition.Select(
                    g => new
                    {     
                        Pair = g.Key,  
                        Score = g.Sum(pair => pair.score) //* competition.coefficient
                    });

                var result = data.OrderBy(datum => datum.Score);

                competition.display(); System.Console.WriteLine();
                foreach (var resultToDisplay in result)
                {
                    resultToDisplay.Pair.display();
                    System.Console.WriteLine("Score is: " + resultToDisplay.Score.ToString());
                }
                System.Console.WriteLine();
            }
        }

        public void displayWinner()
        {
            Dictionary<PairParticipant, double> dict = new Dictionary<PairParticipant, double>();
            foreach (Сompetition competition in competitions)
            {
                var CompetitionProtocols = this.protocols.FindAll(protocol => protocol.competition == competition);
                var pairsInCompetition = CompetitionProtocols.GroupBy(protocol => protocol.pair);
                var data = pairsInCompetition.Select(
                    g => new
                    {      
                        Pair = g.Key,
                        Score = g.Sum(pair => pair.score) * competition.coefficient
                    }
                 );

                foreach (var datum in data)
                {
                    if (dict.ContainsKey(datum.Pair))
                    {
                        dict[datum.Pair] += datum.Score;
                    }
                    else
                    {
                        dict.Add(datum.Pair, datum.Score);
                    }
                }
            }

            var result = dict.GroupBy(x => x.Key).Select(
                g => new
                {
                    Pair = g.Key,
                    Score = g.Sum(x => x.Value)
                }
            ).OrderBy(x => x.Score);

            System.Console.Write("Number of pairs is: ");
            System.Console.WriteLine(result.Count());
            System.Console.WriteLine("Winner is: ");
            result.First().Pair.display();
            System.Console.WriteLine();
            System.Console.Write("Score is: ");
            System.Console.WriteLine(result.First().Score);

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Participant man1 = new Participant("Vanya", "kozlov", true);
            Participant woman1 = new Participant("girl", "kozlova", false);
            PairParticipant pair1 = new PairParticipant("krevedko", man1, woman1);

            Participant man2 = new Participant("Pupkov", "Vasya", true);
            Participant woman2 = new Participant("Tyan", "New", false);
            PairParticipant pair2 = new PairParticipant("2ch.hk/b", man2, woman2);

            Judge judge1 = new Judge("666", "228", true);
            Judge judge2 = new Judge("Body", "Positive", false);

            Сompetition kvalificationCompet = new Сompetition("квалификация", 6, 0.2);
            Сompetition shortCompet = new Сompetition("короткая", 6, 0.3);
            Сompetition freeCompet = new Сompetition("произвольная программа", 6, 0.5);

            Сhampionship championship = new Сhampionship();

            championship.addCompetition(kvalificationCompet);
            championship.addCompetition(shortCompet);
            championship.addCompetition(freeCompet);

            JudgeProtocol protocol1 = new JudgeProtocol(judge1, pair1, kvalificationCompet, 3);
            JudgeProtocol protocol2 = new JudgeProtocol(judge1, pair1, shortCompet, 1);
            JudgeProtocol protocol3 = new JudgeProtocol(judge1, pair1, freeCompet, 4);

            JudgeProtocol protocol4 = new JudgeProtocol(judge1, pair2, kvalificationCompet, 1);
            JudgeProtocol protocol5 = new JudgeProtocol(judge1, pair2, shortCompet, 1);
            JudgeProtocol protocol6 = new JudgeProtocol(judge1, pair2, freeCompet, 1);

            JudgeProtocol protocol7 = new JudgeProtocol(judge2, pair1, kvalificationCompet, 1);
            JudgeProtocol protocol8 = new JudgeProtocol(judge2, pair1, shortCompet, 1);
            JudgeProtocol protocol9 = new JudgeProtocol(judge2, pair1, freeCompet, 1);

            JudgeProtocol protocol10 = new JudgeProtocol(judge2, pair2, kvalificationCompet, 3);
            JudgeProtocol protocol11 = new JudgeProtocol(judge2, pair2, shortCompet, 3);
            JudgeProtocol protocol12 = new JudgeProtocol(judge2, pair2, freeCompet, 3);

            championship.addProtocol(protocol1);
            championship.addProtocol(protocol2);
            championship.addProtocol(protocol3);
            championship.addProtocol(protocol4);
            championship.addProtocol(protocol5);
            championship.addProtocol(protocol6);
            championship.addProtocol(protocol7);
            championship.addProtocol(protocol8);
            championship.addProtocol(protocol9);
            championship.addProtocol(protocol10);
            championship.addProtocol(protocol11);
            championship.addProtocol(protocol12);

            championship.displayWinner();
            System.Console.ReadLine();
        }
    }
}