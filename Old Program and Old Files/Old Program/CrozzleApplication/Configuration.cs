﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace CrozzleApplication
{
    class Configuration
    {
        #region constants
        public static String allowedCharacters = @"^[a-zA-Z]+$";
        public static String allowedBooleans = @"^(true|false)$";
        private static readonly Char[] PointSeparators = new Char[] { ',' };

        public static string SECTION_LOGFILE = "LOGFILE";
        public static string SECTION_SEQUENCES_IN_FILE = "SEQUENCES-IN-FILE";
        public static string SECTION_CROZZLE_OUTPUT = "CROZZLE-OUTPUT";
        public static string SECTION_CROZZLE_SIZE = "CROZZLE-SIZE";
        public static string SECTION_SEQUENCES_IN_CROZZLE = "SEQUENCES-IN-CROZZLE";
        public static string SECTION_INTERSECTIONS_IN_SEQUENCE = "INTERSECTIONS-IN-SEQUENCE";
        public static string SECTION_DUPLICATE_SEQUENCES = "DUPLICATE-SEQUENCES";
        public static string SECTION_VALID_GROUPS = "VALID-GROUPS";
        public static string SECTION_INTERSECTING_POINTS = "INTERSECTING-POINTS";
        public static string SECTION_NON_INTERSECTING_POINTS = "NON-INTERSECTING-POINTS";
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }

        public String FileErrorsTXT
        {
            get
            {
                int errorNumber = 1;
                String errors = "START PROCESSING FILE: " + ConfigurationFileName + "\r\n";

                foreach (String error in Configuration.Errors)
                    errors += "error " + errorNumber++ + ": " + error + "\r\n";
                errors += "END PROCESSING FILE: " + ConfigurationFileName + "\r\n";

                return (errors);
            }
        }

        public String FileErrorsHTML
        {
            get
            {
                int errorNumber = 1;
                String errors = "<p style=\"font-weight:bold\">START PROCESSING FILE: " + ConfigurationFileName + "</p>";

                foreach (String error in Configuration.Errors)
                    errors += "<p>error " + errorNumber++ + ": " + error + "</p>";
                errors += "<p style=\"font-weight:bold\">END PROCESSING FILE: " + ConfigurationFileName + "</p>";

                return (errors);
            }
        }
        #endregion

        #region properties - configuration file validity
        public Boolean Valid { get; set; } = false;
        #endregion

        #region properties - file names
        public String ConfigurationPath { get; set; }
        public String ConfigurationFileName { get; set; }
        public String ConfigurationDirectoryName { get; set; }
        public String LogFileName { get; set; }
        #endregion

        #region properties - word list configurations
        // Limits on the size of a word list.
        public int MinimumNumberOfUniqueWords { get; set; }
        public int MaximumNumberOfUniqueWords { get; set; }
        #endregion

        #region properties - crozzle output configurations
        public String InvalidCrozzleScore { get; set; } = "";
        public Boolean Uppercase { get; set; } = true;
        public String Style { get; set; } = @"<style></style>";
        public String BGcolourEmptyTD { get; set; } = @"#ffffff";
        public String BGcolourNonEmptyTD { get; set; } = @"#ffffff";
        #endregion

        #region properties - configurations keys
        private static Boolean[] ActualIntersectingKeys { get; set; }
        private static Boolean[] ActualNonIntersectingKeys { get; set; }
        private static List<string> ActualKeys { get; set; }
        private static readonly List<string> ExpectedKeys = new List<string>()
        {
            ConfigurationKeys.LOGFILE_NAME,
            ConfigurationKeys.MINIMUM,
            ConfigurationKeys.MAXIMUM,
            ConfigurationKeys.MINIMUM_NUMBER_OF_UNIQUE_WORDS,
            ConfigurationKeys.MAXIMUM_NUMBER_OF_UNIQUE_WORDS,
            ConfigurationKeys.INVALID_CROZZLE_SCORE,
            ConfigurationKeys.UPPERCASE,
            ConfigurationKeys.STYLE,
            ConfigurationKeys.BGCOLOUR_EMPTY_TD,
            ConfigurationKeys.BGCOLOUR_NON_EMPTY_TD,
            ConfigurationKeys.MINIMUM_NUMBER_OF_ROWS,
            ConfigurationKeys.MAXIMUM_NUMBER_OF_ROWS,
            ConfigurationKeys.MINIMUM_NUMBER_OF_COLUMNS,
            ConfigurationKeys.MAXIMUM_NUMBER_OF_COLUMNS,
            ConfigurationKeys.MINIMUM_HORIZONTAL_WORDS,
            ConfigurationKeys.MAXIMUM_HORIZONTAL_WORDS,
            ConfigurationKeys.MINIMUM_VERTICAL_WORDS,
            ConfigurationKeys.MAXIMUM_VERTICAL_WORDS,
            ConfigurationKeys.MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS,
            ConfigurationKeys.MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS,
            ConfigurationKeys.MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS,
            ConfigurationKeys.MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS,
            ConfigurationKeys.MINIMUM_NUMBER_OF_THE_SAME_WORD,
            ConfigurationKeys.MAXIMUM_NUMBER_OF_THE_SAME_WORD,
            ConfigurationKeys.MINIMUM_NUMBER_OF_GROUPS,
            ConfigurationKeys.MAXIMUM_NUMBER_OF_GROUPS,
            ConfigurationKeys.POINTS_PER_WORD,
            ConfigurationKeys.INTERSECTING_POINTS_PER_LETTER,
            ConfigurationKeys.NON_INTERSECTING_POINTS_PER_LETTER
        };
        #endregion

        #region properties - crozzle configurations
        // Limits on the size of the crozzle grid.
        public int MinimumNumberOfRows { get; set; }
        public int MaximumNumberOfRows { get; set; }
        public int MinimumNumberOfColumns { get; set; }
        public int MaximumNumberOfColumns { get; set; }

        // Limits on the number of horizontal and vertical words in a crozzle.
        public int MinimumHorizontalWords { get; set; }
        public int MaximumHorizontalWords { get; set; }
        public int MinimumVerticalWords { get; set; }
        public int MaximumVerticalWords { get; set; }

        // Limits on the number of 
        // intersecting vertical words for each horizontal word, and
        // intersecting horizontal words for each vertical word.
        public int MinimumIntersectionsInHorizontalWords { get; set; }
        public int MaximumIntersectionsInHorizontalWords { get; set; }
        public int MinimumIntersectionsInVerticalWords { get; set; }
        public int MaximumIntersectionsInVerticalWords { get; set; }

        // Limits on duplicate words in the crozzle.
        public int MinimumNumberOfTheSameWord { get; set; }
        public int MaximumNumberOfTheSameWord { get; set; }

        // Limits on the number of valid word groups.
        public int MinimumNumberOfGroups { get; set; }
        public int MaximumNumberOfGroups { get; set; }
        #endregion

        #region properties - scoring configurations
        // The number of points per word within the crozzle.
        public int PointsPerWord { get; set; }

        // Points per letter that is at the intersection of a horizontal and vertical word within the crozzle.
        public int[] IntersectingPointsPerLetter { get; set; } = new int[26];

        // Points per letter that is not at the intersection of a horizontal and vertical word within the crozzle.
        public int[] NonIntersectingPointsPerLetter { get; set; } = new int[26];
        #endregion

        #region constructors
        public Configuration(String path)
        {
            ConfigurationPath = path;
            ConfigurationFileName = Path.GetFileName(path);
            ConfigurationDirectoryName = Path.GetDirectoryName(path);
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String path, out Configuration aConfiguration)
        {
            Errors = new List<String>();
            ActualIntersectingKeys = new Boolean[26];
            ActualNonIntersectingKeys = new Boolean[26];
            ActualKeys = new List<string>();
            aConfiguration = new Configuration(path);

            // Check file name
            if (aConfiguration.ConfigurationFileName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
                Errors.Add(String.Format(ConfigurationErrors.FilenameError, path));
            // Open file
            else
            {
                StreamReader fileIn = new StreamReader(path);
                String section = null;
                bool newBlock = false; 

                // Validate file.
                while (!fileIn.EndOfStream)
                {
                    // Read a line.
                    String line = fileIn.ReadLine();

                    // Processing empty lines
                    if (Regex.IsMatch(line, @"^\s*$"))
                        continue;
                    line = line.Trim();

                    // Processing comments
                    if (line.Contains("//"))
                    {
                        if (line.StartsWith("//"))
                        {
                            continue;
                        }
                        else
                        {
                            Errors.Add(String.Format(ConfigurationErrors.MixConfigWithComentError, line));
                            continue;
                        }
                    }

                    // Section check
                    switch (line)
                    {
                        case "LOGFILE":
                        case "SEQUENCES-IN-FILE":
                        case "CROZZLE-OUTPUT":
                        case "CROZZLE-SIZE":
                        case "SEQUENCES-IN-CROZZLE":
                        case "INTERSECTIONS-IN-SEQUENCES":
                        case "DUPLICATE-SEQUENCES":
                        case "VALID-GROUPS":
                        case "INTERSECTING-POINTS":
                        case "NON-INTERSECTING-POINTS":
                            section = line;
                            newBlock = true;
                            break;
                        case "END-LOGFILE":
                        case "END-SEQUENCES-IN-FILE":
                        case "END-CROZZLE-OUTPUT":
                        case "END-CROZZLE-SIZE":
                        case "END-SEQUENCES-IN-CROZZLE":
                        case "END-INTERSECTIONS-IN-SEQUENCES":
                        case "END-DUPLICATE-SEQUENCES":
                        case "END-VALID-GROUPS":
                        case "END-INTERSECTING-POINTS":
                        case "END-NON-INTERSECTING-POINTS":
                            section = null;
                            newBlock = true;
                            break;
                        default:
                            break;
                    }
                    
                    if (newBlock)
                    {
                        newBlock = false;
                        continue;
                    }

                    // Parse the config item
                    ConfigurationFileItem aConfigurationItem;

                    // Out of section
                    if (section == null)
                    {
                        Errors.Add(String.Format(ConfigurationErrors.OutOfSectionError, line));
                    }
                    
                    // Parse a configuration item.
                    else if (ConfigurationFileItem.TryParse(line, out aConfigurationItem))
                    {
                        // Remove duplicate
                        if (aConfigurationItem.KeyValue != null && ActualKeys.Contains(aConfigurationItem.KeyValue.Key)
                            && !aConfigurationItem.KeyValue.Key.StartsWith("MAXIMUM") && !aConfigurationItem.KeyValue.Key.StartsWith("MINIMUM"))
                            Errors.Add(String.Format(ConfigurationErrors.DuplicateKeyError, aConfigurationItem.KeyValue.OriginalKeyValue));
                        // Parse data
                        else
                        {
                            // Record that this key has been found.
                            if(aConfigurationItem.KeyValue != null)
                                ActualKeys.Add(aConfigurationItem.KeyValue.Key);

                            // Process the key-value.
                            if (aConfigurationItem.IsLogFile)
                            {
                                // Check section
                                if(section != SECTION_LOGFILE)
                                {
                                    Errors.Add(String.Format(ConfigurationErrors.WrongSectionError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                    break;
                                }

                                // Remove delimeter
                                aConfiguration.LogFileName = aConfigurationItem.KeyValue.Value.Trim();
                                if (Validator.IsDelimited(aConfiguration.LogFileName, Crozzle.StringDelimiters))
                                {
                                    aConfiguration.LogFileName = aConfiguration.LogFileName.Trim(Crozzle.StringDelimiters);
                                    if (!Validator.IsFilename(aConfiguration.LogFileName))
                                        Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }

                            

                            else if (aConfigurationItem.IsInvalidCrozzleScore)
                            {
                                // Get the value representing an invalid score.
                                aConfiguration.InvalidCrozzleScore = aConfigurationItem.KeyValue.Value.Trim();
                                if (Validator.IsDelimited(aConfiguration.InvalidCrozzleScore, Crozzle.StringDelimiters))
                                    aConfiguration.InvalidCrozzleScore = aConfiguration.InvalidCrozzleScore.Trim(Crozzle.StringDelimiters);
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsUppercase)
                            {
                                // Get the Boolean value that determines whether to display the crozzle letters in uppercase or lowercase.
                                Boolean uppercase = true;
                                if (!Validator.IsMatch(aConfigurationItem.KeyValue.Value, allowedBooleans))
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                aConfiguration.Uppercase = uppercase;
                            }
                            else if (aConfigurationItem.IsStyle)
                            {
                                // Get the value of the HTML style to display the crozzle in an HTML table.
                                aConfiguration.Style = aConfigurationItem.KeyValue.Value.Trim();
                                if (Validator.IsDelimited(aConfiguration.Style, Crozzle.StringDelimiters))
                                {
                                    aConfiguration.Style = aConfiguration.Style.Trim(Crozzle.StringDelimiters);
                                    if (!Validator.IsStyleTag(aConfiguration.Style))
                                        Errors.Add(String.Format(ConfigurationErrors.StyleError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsBGcolourEmptyTD)
                            {
                                // Get the value of the background colour for an empty TD (HTML table data).
                                aConfiguration.BGcolourEmptyTD = aConfigurationItem.KeyValue.Value.Trim();
                                if (!Validator.IsHexColourCode(aConfiguration.BGcolourEmptyTD))
                                    Errors.Add(String.Format(ConfigurationErrors.ColourError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsBGcolourNonEmptyTD)
                            {
                                // Get the value of the background colour for a non empty TD (HTML table data).
                                aConfiguration.BGcolourNonEmptyTD = aConfigurationItem.KeyValue.Value.Trim();
                                if (!Validator.IsHexColourCode(aConfiguration.BGcolourNonEmptyTD))
                                    Errors.Add(String.Format(ConfigurationErrors.ColourError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumNumberOfRows)
                            {
                                // Get the value of the minimum number of rows per crozzle.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumNumberOfRows = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumNumberOfRows)
                            {
                                // Get the value of the maximum number of rows per crozzle.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumNumberOfRows = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumNumberOfColumns)
                            {
                                // Get the value of the minimum number of columns per crozzle.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumNumberOfColumns = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumNumberOfColumns)
                            {
                                // Get the value of the maximum number of columns per crozzle.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumNumberOfColumns = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumHorizontalWords)
                            {
                                // Get the value of the minimum number of horizontal words in a crozzle.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumHorizontalWords = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumHorizontalWords)
                            {
                                // Get the value of the maximum number of horizontal words in a crozzle.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumHorizontalWords = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumVerticalWords)
                            {
                                // Get the value of the minimum number of vertical words in a crozzle.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumVerticalWords = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumVerticalWords)
                            {
                                // Get the value of the maximum number of vertical words in a crozzle.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumVerticalWords = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumIntersectionsInHorizontalWords)
                            {
                                // Get the value of the minimum number of the intersections in a horizontal word.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumIntersectionsInHorizontalWords = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumIntersectionsInHorizontalWords)
                            {
                                // Get the value of the maximum number of the intersections in a horizontal word.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumIntersectionsInHorizontalWords = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMinimumIntersectionsInVerticalWords)
                            {
                                // Get the value of the minimum number of the intersections in a vertical word.
                                int minimum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum))
                                {
                                    aConfiguration.MinimumIntersectionsInVerticalWords = minimum;
                                    if (!Validator.TryRange(minimum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsMaximumIntersectionsInVerticalWords)
                            {
                                // Get the value of the maximum number of the intersections in a vertical word.
                                int maximum;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum))
                                {
                                    aConfiguration.MaximumIntersectionsInVerticalWords = maximum;
                                    if (!Validator.TryRange(maximum, 1, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }

                            else if (aConfigurationItem.IsPointsPerWord)
                            {
                                // Get the value of points per words.
                                int pointsPerWord;
                                if (Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out pointsPerWord))
                                {
                                    aConfiguration.PointsPerWord = pointsPerWord;
                                    if (!Validator.TryRange(pointsPerWord, 0, Int32.MaxValue))
                                        Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                            }
                            else if (aConfigurationItem.IsIntersecting)
                            {
                                // Get the values of each INTERSECTING point.
                                String originalValues = aConfigurationItem.KeyValue.Value.Trim();
                                if (Validator.IsDelimited(originalValues, Crozzle.StringDelimiters))
                                    originalValues = originalValues.Trim(Crozzle.StringDelimiters).Trim();
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, originalValues, Validator.Errors[0]));

                                String[] intersectingPoints = originalValues.Split(PointSeparators);
                                foreach (String intersectingPoint in intersectingPoints)
                                {
                                    KeyValue aKeyValue;
                                    if (KeyValue.TryParse(intersectingPoint, @"[A-Z]", out aKeyValue))
                                    {
                                        int points;
                                        if (Validator.IsInt32(aKeyValue.Value, out points))
                                        {
                                            int index = (int)aKeyValue.Key[0] - (int)'A';
                                            aConfiguration.IntersectingPointsPerLetter[index] = points;
                                            ActualIntersectingKeys[index] = true;
                                        }
                                        else
                                            Errors.Add(String.Format(ConfigurationErrors.ValueError, aKeyValue.OriginalKeyValue, Validator.Errors[0]));
                                    }
                                    else
                                        Errors.AddRange(KeyValue.Errors);
                                }
                            }
                            else if (aConfigurationItem.IsNonIntersecting)
                            {
                                // Get the value of each NONINTERSECTING point.
                                String originalValues = aConfigurationItem.KeyValue.Value.Trim();
                                if (Validator.IsDelimited(originalValues, Crozzle.StringDelimiters))
                                    originalValues = originalValues.Trim(Crozzle.StringDelimiters).Trim();
                                else
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, originalValues, Validator.Errors[0]));

                                String[] nonIntersectingPoints = originalValues.Split(PointSeparators);
                                foreach (String nonIntersectingPoint in nonIntersectingPoints)
                                {
                                    KeyValue aKeyValue;
                                    if (KeyValue.TryParse(nonIntersectingPoint, @"[A-Z]", out aKeyValue))
                                    {
                                        int points;
                                        if (Validator.IsInt32(aKeyValue.Value, out points))
                                        {
                                            int index = (int)aKeyValue.Key[0] - (int)'A';
                                            aConfiguration.NonIntersectingPointsPerLetter[index] = points;
                                            ActualNonIntersectingKeys[index] = true;
                                        }
                                        else
                                            Errors.Add(String.Format(ConfigurationErrors.ValueError, aKeyValue.OriginalKeyValue, Validator.Errors[0]));
                                    }
                                    else
                                        Errors.AddRange(KeyValue.Errors);
                                }
                            }


                            // Minumum
                            else if (aConfigurationItem.IsMinimum)
                            {
                                // Process value
                                int minimum;
                                if (!Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out minimum)) // if not number
                                {
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                    continue;
                                }
                                else if (!Validator.TryRange(minimum, 1, Int32.MaxValue)) // if number out of range
                                {
                                    Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }


                                // Switch + save
                                switch (section)
                                {
                                    case "SEQUENCES-IN-FILE":
                                        aConfiguration.MaximumNumberOfUniqueWords = minimum;
                                        break;
                                    case "DUPLICATE-SEQUENCES":
                                        aConfiguration.MinimumNumberOfTheSameWord = minimum;
                                        break;
                                    case "VALID-GROUPS":
                                        aConfiguration.MinimumNumberOfGroups = minimum;
                                        break;
                                    default:
                                        Errors.Add(String.Format(ConfigurationErrors.WrongSectionError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0])); ;
                                        break;
                                }

                            }

                            // Maximum
                            else if (aConfigurationItem.IsMaximum)
                            {
                                // Process value
                                int maximum;
                                if (!Validator.IsInt32(aConfigurationItem.KeyValue.Value.Trim(), out maximum)) // if not number
                                {
                                    Errors.Add(String.Format(ConfigurationErrors.ValueError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                    continue;
                                }
                                else if (!Validator.TryRange(maximum, 1, Int32.MaxValue)) // if number out of range
                                {
                                    Errors.Add(String.Format(ConfigurationErrors.IntegerError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0]));
                                }


                                // Switch + save
                                switch (section)
                                {
                                    case "SEQUENCES-IN-FILE":
                                        aConfiguration.MaximumNumberOfUniqueWords = maximum;
                                        break;
                                    case "DUPLICATE-SEQUENCES":
                                        aConfiguration.MinimumNumberOfTheSameWord = maximum;
                                        break;
                                    case "VALID-GROUPS":
                                        aConfiguration.MinimumNumberOfGroups = maximum;
                                        break;
                                    default:
                                        Errors.Add(String.Format(ConfigurationErrors.WrongSectionError, aConfigurationItem.KeyValue.OriginalKeyValue, Validator.Errors[0])); ;
                                        break;
                                }

                            }
                        }
                    }
                    else
                        Errors.AddRange(ConfigurationFileItem.Errors);
                }

                // Close files.
                fileIn.Close();

                // Check which keys are missing from the configuration file.
                foreach (string expectedKey in ExpectedKeys)
                    if (!ActualKeys.Contains(expectedKey) && !expectedKey.StartsWith("MINIMUM") && !expectedKey.StartsWith("MAXIMUM"))
                        Errors.Add(String.Format(ConfigurationErrors.MissingKeyError, expectedKey));
                for (char ch = 'A'; ch <= 'Z'; ch++)
                    if (!ActualIntersectingKeys[(int)ch - (int)'A'])
                        Errors.Add(String.Format(ConfigurationErrors.MissingIntersectionKeyError, ch.ToString()));
                for (char ch = 'A'; ch <= 'Z'; ch++)
                    if (!ActualNonIntersectingKeys[(int)ch - (int)'A'])
                        Errors.Add(String.Format(ConfigurationErrors.MissingNonIntersectionKeyError, ch.ToString()));

                // Check that minimum values are <= to their maximmum counterpart values.
                if (ActualKeys.Contains("MINIMUM_NUMBER_OF_UNIQUE_WORDS") && ActualKeys.Contains("MAXIMUM_NUMBER_OF_UNIQUE_WORDS"))
                    if (aConfiguration.MinimumNumberOfUniqueWords > aConfiguration.MaximumNumberOfUniqueWords)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_NUMBER_OF_UNIQUE_WORDS",
                            aConfiguration.MinimumNumberOfUniqueWords, aConfiguration.MaximumNumberOfUniqueWords));
                if (ActualKeys.Contains("MINIMUM_NUMBER_OF_ROWS") && ActualKeys.Contains("MAXIMUM_NUMBER_OF_ROWS"))
                    if (aConfiguration.MinimumNumberOfRows > aConfiguration.MaximumNumberOfRows)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_NUMBER_OF_ROWS",
                            aConfiguration.MinimumNumberOfRows, aConfiguration.MaximumNumberOfRows));
                if (ActualKeys.Contains("MINIMUM_NUMBER_OF_COLUMNS") && ActualKeys.Contains("MAXIMUM_NUMBER_OF_COLUMNS"))
                    if (aConfiguration.MinimumNumberOfColumns > aConfiguration.MaximumNumberOfColumns)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_NUMBER_OF_COLUMNS",
                            aConfiguration.MinimumNumberOfColumns, aConfiguration.MaximumNumberOfColumns));
                if (ActualKeys.Contains("MINIMUM_HORIZONTAL_WORDS") && ActualKeys.Contains("MAXIMUM_HORIZONTAL_WORDS"))
                    if (aConfiguration.MinimumHorizontalWords > aConfiguration.MaximumHorizontalWords)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_HORIZONTAL_WORDS",
                            aConfiguration.MinimumHorizontalWords, aConfiguration.MaximumHorizontalWords));
                if (ActualKeys.Contains("MINIMUM_VERTICAL_WORDS") && ActualKeys.Contains("MAXIMUM_VERTICAL_WORDS"))
                    if (aConfiguration.MinimumVerticalWords > aConfiguration.MaximumVerticalWords)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_VERTICAL_WORDS",
                            aConfiguration.MinimumVerticalWords, aConfiguration.MaximumVerticalWords));
                if (ActualKeys.Contains("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS") && ActualKeys.Contains("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"))
                    if (aConfiguration.MinimumIntersectionsInHorizontalWords > aConfiguration.MaximumIntersectionsInHorizontalWords)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS",
                            aConfiguration.MinimumIntersectionsInHorizontalWords, aConfiguration.MaximumIntersectionsInHorizontalWords));
                if (ActualKeys.Contains("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS") && ActualKeys.Contains("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"))
                    if (aConfiguration.MinimumIntersectionsInVerticalWords > aConfiguration.MaximumIntersectionsInVerticalWords)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS",
                            aConfiguration.MinimumIntersectionsInVerticalWords, aConfiguration.MaximumIntersectionsInVerticalWords));
                if (ActualKeys.Contains("MINIMUM_NUMBER_OF_THE_SAME_WORD") && ActualKeys.Contains("MAXIMUM_NUMBER_OF_THE_SAME_WORD"))
                    if (aConfiguration.MinimumNumberOfTheSameWord > aConfiguration.MaximumNumberOfTheSameWord)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_NUMBER_OF_THE_SAME_WORD",
                            aConfiguration.MinimumNumberOfTheSameWord, aConfiguration.MaximumNumberOfTheSameWord));
                if (ActualKeys.Contains("MINIMUM_NUMBER_OF_GROUPS") && ActualKeys.Contains("MAXIMUM_NUMBER_OF_GROUPS"))
                    if (aConfiguration.MinimumNumberOfGroups > aConfiguration.MaximumNumberOfGroups)
                        Errors.Add(String.Format(ConfigurationErrors.MinGreaterThanMaxError, "MINIMUM_NUMBER_OF_GROUPS",
                            aConfiguration.MinimumNumberOfGroups, aConfiguration.MaximumNumberOfGroups));
            }

            // Store validity.
            aConfiguration.Valid = Errors.Count == 0;
            return (aConfiguration.Valid);
        }
        #endregion    
    }
}