import scala.collection.mutable.{Map, SortedMap}
class testClass {
//  val x = new LRUCache[Int, Int](1)
}


class LRUCache[K , V](private val capacity: Int)extends Map[K, V]:

  private var cache = SortedMap.empty[K, V]

  def lru: Seq[K] = this.cache.keys.toSeq

  def length: Int = this.cache.size

  override def get(key: K): Option[V] = this.cache.get(key)

  override def getOrElse[V1 >: V](key: K, default: => V1): V1 = this.get(key) match
    case Some(value) => value
    case None => default

  override def iterator: Iterator[(K, V)] = ???

  override def addOne(elem: (K, V)): LRUCache.this.type =
    if this.cache.size < this.capacity then
      this.cache.addOne(elem)
    this

  override def subtractOne(elem: K): LRUCache.this.type = ???

  override def clear(): Unit = cache.clear()

  override def contains(key1: K): Boolean = cache.contains(key1)