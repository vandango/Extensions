using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extensions.Basics;

namespace Extensions.Collections
{
	public static class LinkedListExtensions
	{
		/// <summary>
		/// Add a range of items to the instance of this linked list
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="list">The list that should be added to the LinkedList.</param>
		/// <returns>Returns the list with the added list.</returns>
		public static void AddRange<T>(this LinkedList<T> instance, IEnumerable<T> list)
		{
			foreach(T item in list)
			{
				instance.AddLast(item);
			}
		}

		/// <summary>
		/// Add a range of items to the instance of this linked list
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="list">The list that should be added to the LinkedList.</param>
		/// <returns>Returns the list with the added list.</returns>
		public static void AddRange<T>(this LinkedList<T> instance, LinkedList<T> list)
		{
			foreach(T item in list)
			{
				instance.AddLast(item);
			}
		}

		/// <summary>
		/// Add an item to a specific index of the list
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="index">The target index of the object.</param>
		/// <param name="value">The object that should be added to the target index.</param>
		public static void ToIndex<T>(this LinkedList<T> instance, int index, T value)
		{
			instance.AddAfter(instance.GetNodeFromIndex(index), value);
		}

		/// <summary>
		/// Adds an new item to a linked list
		/// </summary>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <param name="instance">The instance of the linked list.</param>
		/// <param name="item">The new item top add.</param>
		public static void Add<T>(this LinkedList<T> instance, T item)
		{
			if(instance == null)
			{
				instance = new LinkedList<T>();
			}

			instance.AddLast(item);
		}

		/// <summary>
		/// Get a node from a specific index
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="index">The index of the target object.</param>
		/// <returns>Returns a single node from the given index.</returns>
		public static LinkedListNode<T> GetNodeFromIndex<T>(this LinkedList<T> instance, int index)
		{
			if(instance == null)
			{
				throw new ArgumentNullException("The list cannot be null!");
			}

			if(index < 0
			|| index > instance.Count)
			{
				throw new IndexOutOfRangeException(
					index < 0
					? "The index must be greater then 0!"
					: "The index of {0} cannot be greater then the maximal amount of items ({1})!".FormatIt(index, instance.Count)
				);
			}

			if(index == 0)
			{
				return instance.First;
			}
			else
			{
				int count = 0;

				if(index > Math.Round(Convert.ToDouble(instance.Count) / 2D, MidpointRounding.AwayFromZero))
				{
					count = instance.Count;
					index++;

					for(LinkedListNode<T> node = instance.Last; node != null; )
					{
						if(index == count)
						{
							return node;
						}
						else
						{
							node = node.Previous;
							count--;
						}
					}
				}
				else
				{
					for(LinkedListNode<T> node = instance.First; node != null; )
					{
						if(index == count)
						{
							return node;
						}
						else
						{
							node = node.Next;
							count++;
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Gets an item from an index (search via item.Next)
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="index">The index of the target object.</param>
		/// <returns>Returns a single node value from the given index.</returns>
		public static T FromIndex<T>(this LinkedList<T> instance, int index)
		{
			LinkedListNode<T> node = instance.GetNodeFromIndex(index);

			if(node != null)
			{
				return node.Value;
			}
			else
			{
				return default(T);
			}
		}

		/// <summary>
		/// Get a value from an index (search via iteration)
		/// </summary>
		/// <typeparam name="T">The type of the objects of the list.</typeparam>
		/// <param name="instance">The LinkedList object itself.</param>
		/// <param name="index">The index of the target object.</param>
		/// <returns>Returns a single node value from the given index.</returns>
		public static T FromIteratedIndex<T>(this LinkedList<T> instance, int index)
		{
			if(instance == null)
			{
				throw new ArgumentNullException("The list cannot be null!");
			}

			if(index < 0
			|| index > instance.Count)
			{
				throw new IndexOutOfRangeException(
					index < 0
					? "The index must be greater then 0!"
					: "The index of {0} cannot be greater then the maximal amount of items ({1})!".FormatIt(index, instance.Count)
				);
			}

			if(index == 0)
			{
				return instance.First.Value;
			}
			else
			{
				int count = 0;

				if(index > Math.Round(Convert.ToDouble(instance.Count) / 2D, MidpointRounding.AwayFromZero))
				{
					count = instance.Count;
					index++;

					var result = instance.Reverse();

					foreach(T item in result)
					{
						if(index == count)
						{
							return (T)item;
						}

						count--;
					}
				}
				else
				{
					foreach(T item in instance)
					{
						if(index == count)
						{
							return item;
						}

						count++;
					}
				}
			}

			return default(T);
		}
	}
}
