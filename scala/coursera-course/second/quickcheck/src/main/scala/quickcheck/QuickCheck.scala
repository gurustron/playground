package quickcheck

import org.scalacheck._
import Arbitrary._
import Gen._
import Prop._
import scala.math.min

abstract class QuickCheckHeap extends Properties("Heap") with IntHeap {

  lazy val genHeap: Gen[H] = oneOf(
    const(empty),
    for {
      a <- arbitrary[Int]
      h <- oneOf(const(empty), genHeap)
    } yield insert(a, h)
  )

  implicit lazy val arbHeap: Arbitrary[H] = Arbitrary(genHeap)

  def traverse(heap: H): List[Int] = heap match {
    case _ if isEmpty(heap) => List.empty
    case _ => findMin(heap) :: traverse(deleteMin(heap))
  }

  property("gen1") = forAll { (h: H) =>
    val m = if (isEmpty(h)) 0 else findMin(h)
    findMin(insert(m, h)) == m
  }

  property("min1") = forAll { a: Int =>
    val h = insert(a, empty)
    findMin(h) == a
  }

  property("min2") = forAll { (a: Int, b: Int) =>
    val h = insert(b, insert(a, empty))
    findMin(h) == min(a, b)
  }

  property("addDeleteEmpty") = forAll { a: Int =>
    deleteMin(insert(a, empty)) == empty
  }

  property("minMeld") = forAll { (l: H, r: H) =>
    if (isEmpty(l) && isEmpty(r))
      true
    else {
      val meldedMin = findMin(meld(l, r))
      (l, r) match {
        case (h, h1) if isEmpty(h1) => meldedMin == findMin(h)
        case (h1, h) if isEmpty(h1) => meldedMin == findMin(h)
        case _ => meldedMin == min(findMin(l), findMin(r))
      }
    }
  }

  property("genSorted") = forAll { (h: H) =>
    if (isEmpty(h))
      true
    else {
      val traversed = traverse(h)
      traversed == traversed.sorted
    }
  }

  property("addDelete") = forAll { h: H =>
    deleteMin(insert(Int.MinValue, h)) == h
  }

  property("associativeMeld") = forAll { (h: H, i: H, j: H) =>
    val a = meld(meld(h, i), j)
    val b = meld(h, meld(i, j))
    traverse(a) == traverse(b)
  }
}
