using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
	class Program
	{
		static void Main()
		{
			int Jug1 = 0, Jug2 = 0, target = 0;

			Jug1 = intValidator("X");
			Jug2 = intValidator("Y");
			target = intValidator("Z"); ;

			Console.WriteLine("Path from initial state "
							+ "to solution state :");

			BFS(Jug1, Jug2, target);
		}

			static void BFS(int a, int b, int target)
		{

			// El mapa se usa para almacenar los estados, cada estado se convierte en un valor binario para indicar si ese estado se visitó antes o no.
			Dictionary<Tuple<int, int>, int> m
				= new Dictionary<Tuple<int, int>, int>();
			bool isSolvable = false;
			List<Tuple<int, int>> path
				= new List<Tuple<int, int>>();
			// Cola para mantener estados
			List<Tuple<int, int>> q
				= new List<Tuple<int, int>>();

			// Inicializando con estado inicial
			q.Add(new Tuple<int, int>(0, 0));

			while (q.Count > 0)
			{

				// Estado actual
				Tuple<int, int> u = q[0];

				// Pop off estado usado
				q.RemoveAt(0);

				// Si este estado ya está visitado
				if (m.ContainsKey(u) && m[u] == 1)
					continue;

				// No cumple con las restricciones de la jarra
				if ((u.Item1 > a || u.Item2 > b || u.Item1 < 0
					|| u.Item2 < 0))
					continue;

				// Rellenar el vector para construir el camino de la solución
				path.Add(u);

				//Marcar el estado actual como visitado
				m[u] = 1;

				// Si alcanzamos el estado de solución, ponga ans=1
				if (u.Item1 == target || u.Item2 == target)
				{
					isSolvable = true;

					if (u.Item1 == target)
					{
						if (u.Item2 != 0)

							// Fill final state
							path.Add(new Tuple<int, int>(
								u.Item1, 0));
					}
					else
					{
						if (u.Item1 != 0)

							// Fill final state
							path.Add(new Tuple<int, int>(
								0, u.Item2));
					}

					// Print the solution path
					int sz = path.Count;
					for (int i = 0; i < sz; i++)
						Console.WriteLine("(" + path[i].Item1
										+ ", " + path[i].Item2
										+ ")");
					break;
				}

				// If we have not reached final state
				// then, start developing intermediate
				// states to reach solution state
				// Fill Jug2
				q.Add(new Tuple<int, int>(u.Item1, b));

				// Fill Jug1
				q.Add(new Tuple<int, int>(a, u.Item2));

				for (int ap = 0; ap <= Math.Max(a, b); ap++)
				{

					// Pour amount ap from Jug2 to Jug1
					int c = u.Item1 + ap;
					int d = u.Item2 - ap;

					// Check if this state is possible or not
					if (c == a || (d == 0 && d >= 0))
						q.Add(new Tuple<int, int>(c, d));

					// Pour amount ap from Jug 1 to Jug2
					c = u.Item1 - ap;
					d = u.Item2 + ap;

					// Check if this state is possible or not
					if ((c == 0 && c >= 0) || d == b)
						q.Add(new Tuple<int, int>(c, d));
				}

				// Empty Jug2
				q.Add(new Tuple<int, int>(a, 0));

				// Empty Jug1
				q.Add(new Tuple<int, int>(0, b));
			}

			// No, solution exists if ans=0
			if (!isSolvable)
				Console.WriteLine("No solution");
		}

		static int intValidator(string message)
		{
			int value;
			string n;
			bool esNumero;
			do
			{
				Console.WriteLine("Ingrese el valor de " + message + ". Nota: Debe de ser un numero entero y mayor a 0 ");
				n = Console.ReadLine();
				/* Si es número correcto retornará true y saldrá
				   *  del Ciclo*/
				esNumero = int.TryParse(n, out value);
			}
			while (!esNumero || value < 0);

			return value;
		}	

	}
}
