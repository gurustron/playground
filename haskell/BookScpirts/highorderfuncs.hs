compareWith100 :: Int -> Ordering 
compareWith100 = compare 100

divideBy10 :: (Floating a)=> a -> a
divideBy10 = (/10)

isUpper :: Char -> Bool
isUpper = (`elem` ['A'..'Z'])

subtract5 :: (Num a) => a -> a
subtract5 = (subtract 5)

applyTwice :: (a->a) -> a -> a
applyTwice f x = f (f x)

zipWith' :: (a->b->c) -> [a]->[b]->[c]
zipWith' _ _ [] = []
zipWith' _ [] _ = []
zipWith' f (x:xs) (y:ys) = f x y : zipWith f xs ys

flip' :: (a->b->c) -> (b->a->c)
-- flip' f = g
--     where g x y = f y x
flip' f x y = f y x

quicksort :: (Ord a) => [a] -> [a]
quicksort [] = []
quicksort (x:xs) = 
    let smallerOrEqual = filter (x>=) xs
        larger = filter (x<) xs
    in quicksort smallerOrEqual ++ [x] ++ quicksort larger

largerDiv :: Integer
largerDiv = head (filter p [100000,99999..])
    where p x = mod x 3289 == 0  

collatzChain :: Integer -> [Integer]
collatzChain 1 = [1]
collatzChain x
    | even x = x : collatzChain (div x 2)
    | otherwise = x : collatzChain (x*3+1)

numLongChains :: Int
numLongChains = length $ filter isLong $ map collatzChain [1..100]
    where isLong xs = length xs > 15

numLongChains' :: Int
numLongChains' = length $ filter (\x -> length x > 15) $ map collatzChain [1..100]

flip'' :: (a->b->c) -> b->a->c
flip'' f = \x y -> f y x

-- sum' :: (Num a) => [a] -> a
-- sum' xs = foldl (\acc x -> acc + x) 0 xs
sum' :: (Num a) => [a] -> a
sum' xs = foldl (+) 0 xs

map' :: (a->b) -> [a]->[b]
map' f xs = foldr (\x acc -> f x : acc) [] xs

-- map'' :: (a->b) -> [a]->[b]
-- map'' f xs = foldl (\acc x ->acc ++ [f x]) [] xs

elem' :: (Eq a) => a -> [a] -> Bool
elem' y ys = foldr (\x acc -> if x == y then True else acc) False ys

reverse' :: [a] -> [a]
reverse' = foldl (flip (:)) []

filter' :: (a->Bool) -> [a]-> [a]
filter' f = foldl (\acc x -> if f x == True then acc ++ [x] else acc) []  

and' :: [Bool] -> Bool
and' = foldr (&&) True 

oddSquareSum :: Integer
oddSquareSum = sum . takeWhile (<10000) . filter odd $ map (^2) [1..]