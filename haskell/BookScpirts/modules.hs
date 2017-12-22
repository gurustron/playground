import qualified Data.List as DL 
import qualified Data.Char as DC
import qualified Data.Map as DM
-- import qualified Geometry as G

-- import Data.List
-- import Data.List (nub, sort)
-- import Data.List hiding (nub)

numUniques :: (Eq a) => [a] -> Int
numUniques = length . DL.nub

wordNums :: String -> [(String,Int)]
wordNums = map (\ws -> (head ws, length ws)) . DL.group . DL.sort . DL.words 

isIn :: (Eq a) => [a] -> [a] -> Bool
needle `isIn` haystack = DL.any (needle `DL.isPrefixOf`) (DL.tails haystack)

encode :: Int -> String -> [Int]
encode e xs = map (+e) . map (DC.ord) $ xs    --[DC.ord 'a']

decode :: Int -> [Int] -> String
decode e is = map (DC.chr) . map (+ negate e) $ is


encode' :: Int -> String -> String
encode' e xs = map (DC.chr) . map (+e) . map (DC.ord) $ xs    --[DC.ord 'a']

decode' :: Int -> String -> String
decode' e is = map (DC.chr) . map (+ negate e) . map (DC.ord) $ is

decode'' :: Int -> String -> String
decode'' e is = encode' (negate e) is

coolNumber :: Maybe Int
coolNumber = DL.find (\z -> digitSum z == 40) [1..]

digitSum :: Int -> Int
digitSum = sum . map DC.digitToInt . show 

firstTo :: Int -> Maybe Int
firstTo n = DL.find (\x -> digitSum x == n) [1..]

phoneBook :: DM.Map String String
phoneBook = DM.fromList $ 
    [("betty", "555-2938")
    ,("bonnie", "452-2928")
    ,("patsy", "493-2928")
    ,("lucille", "205-2928")
    ,("wendy", "939-8282")
    ,("penny", "853-2492")
    ]

string2Digits :: String -> [Int]
string2Digits = map DC.digitToInt . filter DC.isDigit

pBook :: [(String,String)]
pBook =
    [("betty", "555-2938")
    ,("betty", "342-2492")
    ,("bonnie", "452-2928")
    ,("patsy", "493-2928")
    ,("patsy", "943-2929")
    ,("patsy", "827-9162")
    ,("lucille", "205-2928")
    ,("wendy", "939-8282")
    ,("penny", "853-2492")
    ,("penny", "555-2111")
    ]

-- phoneBook2Map :: (Ord k)=> [(k,String)] -> DM.Map k String
-- phoneBook2Map xs = DM.fromListWith add xs
--     where add number1 number2 = number1 ++ ", " ++ number2

phoneBook2Map :: (Ord k)=> [(k,a)] -> DM.Map k [a]
phoneBook2Map xs = DM.fromListWith (++) $ map (\(k,v) -> (k,[v])) xs

-- test :: Float -> Float
-- test = G.sphereVolume
