﻿namespace TheWorld.Models;

public class Region
{
	public string RegionName { get; set; }
	public IEnumerable<string> Countries { get; set; }
}