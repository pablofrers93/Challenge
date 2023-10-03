using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticArm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int clawPos = 1;
            List<int> boxes = new List<int> { 3,2,1,4};
            int boxInClaw = 1;

            List<string> commands = Solve(clawPos, boxes, boxInClaw);

            foreach (string command in commands)
            {
                Console.WriteLine(command);
            }
            
            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }

            Console.ReadLine();
        }
        public static List<string> Solve(int clawPos, List<int> boxes, int boxInClaw)
        {
            List<string> commands = new List<string>();

            int totalBoxes = boxes.Sum() + boxInClaw;
            int numStacks = boxes.Count;
            int targetBoxesPerStack = (totalBoxes + numStacks - 1) / numStacks;

            while (true)
            {
                if (boxes.TrueForAll(box => box == targetBoxesPerStack) || boxes.Take(numStacks - 1).All(box => box == targetBoxesPerStack))
                {
                    break; // Sale del loop cuando esta todo balanceado o todo menos el ultimo.
                }

                if (boxInClaw == 1)
                {
                    // Si el brazo tiene una caja la deja en el stack actual.
                    commands.Add("PLACE");
                    boxes[clawPos]++;
                    boxInClaw = 0;
                }

                if (boxes[clawPos] > targetBoxesPerStack)
                {
                    // Si el stack actual tiene mas cajas que lo permitido toma una de arriba del todo.
                    commands.Add("PICK");
                    boxes[clawPos]--;
                    boxInClaw = 1;
                }

                // Se mueve el brazo al siguiente stack
                if (clawPos < numStacks - 1)
                {
                    commands.Add("RIGHT");
                    clawPos++;
                }
                else
                {
                    // Si el brazo está en la ultima posicion a la derecha, se vuelve a posicionar al comienzo.
                    while (clawPos > 0)
                    {
                        commands.Add("LEFT");
                        clawPos--;
                    }
                }

                //Comprueba si el penultimo stack está desbalanzeado y el ultimo balanceado.
                if (clawPos == numStacks - 2 && 
                    boxes[numStacks - 2] < targetBoxesPerStack && 
                    boxes[numStacks - 1] == targetBoxesPerStack)
                {
                    // Se mueve una caja del ultimo stack al penultimo stack.
                    commands.Add("PICK");
                    commands.Add("LEFT");
                    commands.Add("PLACE");
                    boxes[numStacks - 2]++;
                    boxes[numStacks - 1]--;
                }

            }

            return commands;
        }

    }
}


