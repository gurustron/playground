doubleMe x = x+x

doubleUs x y = doubleMe x + doubleMe 2

multiLine inp = [ [ x | x <- xs, even x ]
 | xs <- inp]

removeNonUppercase :: [Char] -> [Char]
removeNonUppercase st = [ c | c <- st, c `elem` ['A'..'Z']]

addThree :: Int -> Int -> Int -> Int
addThree x y z = x + y + z

factorial :: Integer -> Integer
factorial n = product [1..n]

circumference :: Float -> Float
circumference r = 2 * pi * r

circumference' :: Double -> Double
circumference' r = 2 * pi * r

swap1 :: (a,b) -> (b,a)
swap1 (a,b) = (b,a)

lucky :: Int -> String
lucky 7 = "LUCKY NUMBER SEVEN!"
lucky x = "Sorry, you're out of luck, pal!"

sayMe :: Int -> String
sayMe 1 = "One"
sayMe 2 = "Two"
sayMe x = show x ++ " not One Or Two"

factorial1 :: Int -> Int
factorial1 0 = 1
factorial1 n = n * factorial1(n - 1)

charName :: Char -> String
charName 'a' = "Albert"
charName 'b' = "Broseph"
charName 'c' = "Cecil"

addVectors :: (Double, Double) -> (Double, Double) -> (Double, Double)
addVectors a b = (fst a + fst b, snd a + snd b)

addVectors1 :: (Double, Double) -> (Double, Double) -> (Double, Double)
addVectors1 (x1, y1) (x2, y2) = (x1 + x2, y1 + y2)

first :: (a, b, c) -> a
first (x, _, _) = x
second :: (a, b, c) -> b
second (_, y, _) = y
third :: (a, b, c) -> c
third (_, _, z) = z

head' :: [a] -> a
head' [] = error "Empty list - no head"
head' (x:_) = x

tell :: (Show a) => [a] -> String
tell [] = "Empty"
tell (x:[]) = "One element " ++ show x
tell (x:y:[]) = "Two elements " ++ show x ++ " and " ++ show y
tell (x:y:_) = "Many elements"

firstLetter :: String -> String
firstLetter "" = "Empty"
firstLetter all@(x:xs) = "First letter of " ++ show all ++ " is " ++ show x ++ " and the rest is " ++ show xs

max' :: (Ord a) => a -> a -> a
max' a b
    | a <= b = b
    | otherwise = a

bmiTell :: Double -> String
bmiTell bmi
    | bmi <= 18.5 = "Skinny"
    | bmi <= 25.0 = "Norm"
    | bmi <= 30.0 = "Fat"
    | otherwise = "Whale"

bmiTell1 :: Double -> Double -> String
bmiTell1 w h
    | bmi <= skinny = "Skinny"
    | bmi <= normal = "Norm"
    | bmi <= fat = "Fat"
    | otherwise = "Whale"
    where bmi = w / h^2
          (skinny, normal, fat) = (18.5, 25.0, 30.0)
        --   skinny = 18.5
        --   normal = 25.0
        --   fat = 30.0

myComp :: (Ord a) => a -> a -> Ordering
a `myComp` b
    | a == b = EQ
    | a <= b = LT
    | otherwise = GT

-- greet :: String -> String
-- greet "Juan" = niceGreeting ++ " Juan!"
-- greet "Fernando" = niceGreeting ++ " Fernando!"
-- greet name = badGreeting ++ " " ++ name
--     where niceGreeting = "Hello! So very nice to see you,"
--           badGreeting = "Oh! Pfft. It's you."

badGreeting :: String
badGreeting = "Oh! Pfft. It's you, "
niceGreeting :: String
niceGreeting = "Hello! So very nice to see you,"
greet :: String -> String
greet "Juan" = niceGreeting ++ " Juan!"
greet "Fernando" = niceGreeting ++ " Fernando!"
greet name = badGreeting ++ " " ++ name

greet1 :: String -> String
greet1 name 
    | name == "Juan" = niceGreeting ++ " Juan!"
    | name == "Fernando" = niceGreeting ++ " Fernando!"
    | otherwise = badGreeting ++ " " ++ name
    where niceGreeting1 = "Hello! So very nice to see you,"
          badGreeting1 = "Oh! Pfft. It's you."


-- bmiTell11 :: String-> String
-- bmiTell11 n
--     | n == "Juan" = niceGreeting ++ " Juan!"
--     | n == "Fernando" = niceGreeting ++ " Fernando!"
--     | otherwise = badGreeting ++ " " ++ n
--     where bmi = 1

initials :: String -> String -> String
initials firstName lastName = f : ". " ++ [l] ++ "." 
    where (f:_) = firstName
          (l:_) = lastName

-- calcBmis :: [(Double,Double)] -> [Double]
-- calcBmis xs = [bmi w h|(w,h)<-xs]
--     where bmi we hi = we / hi^2 

calcBmis :: [(Double,Double)] -> [Double]
-- calcBmis xs = [bmi | (w, h) <- xs, let bmi = w / h ^ 2]
calcBmis xs = [bmi |(w,h)<-xs, let bmi = w / h^2, bmi > 25 ]
    -- where bmi we hi = we / hi^2 

cylinder :: Double->Double->Double
cylinder r h = 
    let sideArea = 2*pi*h
        topArea = pi*r^2
in sideArea+2*topArea

head'' :: [a] -> a
head'' xs = case xs of [] -> error "No head for empty lists!"
                    (x:_) -> x