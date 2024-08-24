﻿using System.ComponentModel;
using Moder.Core.Models;
using ParadoxPower.Parser;
using ParadoxPower.Process;

namespace Moder.Core.Extensions;

public static class ParserExtend
{
	public static bool HasNot(this Node node, string key)
	{
		return !node.Has(key);
	}

	public static Node GetChild(this Node node, string key)
	{
		return node.Child(key).Value;
	}

	public static bool TryGetChild(this Node node, string key, out Node child)
	{
		var result = node.Child(key);
		if (result is null)
		{
			child = null!;
			return false;
		}
		else
		{
			child = result.Value;
			return true;
		}
	}

	public static GameValueType ToLocalValueType(this Types.Value value)
	{
		if (value.IsBool)
		{
			return GameValueType.Bool;
		}

		if (value.IsFloat)
		{
			return GameValueType.Float;
		}

		if (value.IsInt)
		{
			return GameValueType.Int;
		}

		if (value.IsString)
		{
			return GameValueType.String;
		}

		if (value.IsQString)
		{
			return GameValueType.StringWithQuotation;
		}

		// if (value.IsClause)
		// {
		//     return GameValueType.Clause;
		// }
		throw new InvalidEnumArgumentException(nameof(value));
	}

	public static string GetKey(this Child child)
	{
		if (child.IsLeafChild)
		{
			return child.leaf.Key;
		}

		if (child.IsNodeChild)
		{
			return child.node.Key;
		}

		if (child.IsLeafValueChild)
		{
			return child.leafvalue.Key;
		}

		if (child.IsCommentChild || child.IsValueClauseChild)
		{
			throw new InvalidOperationException("这个 child 不存在 key");
		}
		throw new InvalidEnumArgumentException(nameof(child));
	}
}