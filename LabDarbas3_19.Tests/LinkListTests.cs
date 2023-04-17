using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using LabDarbas3_19.Class;
using System;
using System.Collections;

namespace LabDarbas3_19.Tests
{
    [TestClass]
    public class LinkListTests
    {
        [TestMethod]
        public void LinkList0ParamsCtorTest()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            list.Count.Should().Be(0);
            list.IsReadOnly.Should().BeFalse();
        }

        [TestMethod]
        public void NullList_NotThrowsException()
        {
            Action act = () => { LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo> { null }; };
            act.Should().NotThrow<NullReferenceException>();
        }

        [TestMethod]
        public void EmptyList_CountIsZero()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item = new GeneralProductInfo();
            list.Count.Should().Be(0);
        }

        [TestMethod]
        public void AddingItemIncreasesCountTest()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item = new GeneralProductInfo();
            list.Add(item);
            list.Count.Should().Be(1);
        }

        [TestMethod]
        public void FourItemsInList_CountIsFour()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item = new GeneralProductInfo();
            list.Add(item);
            list.Add(item);
            list.Add(item);
            list.Add(item);
            list.Count.Should().Be(4);
        }

        [TestMethod]
        public void AddingNull_NotThrowsException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            Action act = () => { list.Add(null); };
            act.Should().NotThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void AddingItem_ListHasThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item);
            IEnumerator enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(item, enumerator.Current);
        }

        [TestMethod]
        public void ClearingList_ListIsEmpty()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Clear();
            list.Count.Should().Be(0);
            IEnumerator enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(null, enumerator.Current);
        }

        [TestMethod]
        public void RemovingNull_NotThrowsException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            Action act = () => { list.Remove(null); };
            act.Should().NotThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void RemovingItem_ListHasNotThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Remove(item2);
            IEnumerator enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreNotEqual(item2, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreNotEqual(item2, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreNotEqual(item2, enumerator.Current);
        }

        [TestMethod]
        public void FindingNull_NotThrowsException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            Action act = () => { list.Find(null); };
            act.Should().NotThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void FindingItem_ListHasNotThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Find(item2).Should().BeNull();
        }

        [TestMethod]
        public void FindingItem_ListHasThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Find(item2).Data.Should().Be(item2);
        }

        [TestMethod]
        public void ContainsNull_NotThrowsException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            Action act = () => { list.Contains(null); };
            act.Should().NotThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void ContainsItem_ListHasNotThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Contains(item2).Should().BeFalse();
        }

        [TestMethod]
        public void ContainsItem_ListHasThatItem()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_1", 10, 1.11f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_2", 20, 2.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            list.Contains(item2).Should().BeTrue();
        }

        [TestMethod]
        public void SortItems_ItemsInOrder()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_3", 10, 2.22f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_3", 20, 7.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            GeneralProductInfo item4 = new GeneralProductInfo("Prekė_1", 10, 0f);
            list.Add(item4);
            GeneralProductInfo item5 = new GeneralProductInfo("Prekė_2", 20, 10.33f);
            list.Add(item5);
            GeneralProductInfo item6 = new GeneralProductInfo("Prekė_2", 30, 3.33f);
            list.Add(item6);
            GeneralProductInfo item7 = new GeneralProductInfo("Prekė_1", 30, 1.33f);
            list.Add(item7);
            list.Sort();
            IEnumerator enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(item4, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item7, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item6, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item5, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item1, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item3, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(item2, enumerator.Current);
        }

        [TestMethod]
        public void CopyListToNull_ThrowException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            Action act = () => { list.CopyTo(null, 0); };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CopyListToArrayBadIndex_ThrowException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo[] array = new GeneralProductInfo[7];
            Action act = () => { list.CopyTo(array, 8); };
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void CopyListToArrayInsfSpace_ThrowException()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_3", 10, 2.22f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_3", 20, 7.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            GeneralProductInfo[] array = new GeneralProductInfo[0];
            Action act = () => { list.CopyTo(array, 0); };
            act.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CopyListToArray_SameItemsInArrayInSameOrder()
        {
            LinkList<GeneralProductInfo> list = new LinkList<GeneralProductInfo>();
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_3", 10, 2.22f);
            list.Add(item1);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_3", 20, 7.22f);
            list.Add(item2);
            GeneralProductInfo item3 = new GeneralProductInfo("Prekė_3", 30, 3.33f);
            list.Add(item3);
            GeneralProductInfo item4 = new GeneralProductInfo("Prekė_1", 10, 0f);
            list.Add(item4);
            GeneralProductInfo item5 = new GeneralProductInfo("Prekė_2", 20, 10.33f);
            list.Add(item5);
            GeneralProductInfo item6 = new GeneralProductInfo("Prekė_2", 30, 3.33f);
            list.Add(item6);
            GeneralProductInfo item7 = new GeneralProductInfo("Prekė_1", 30, 1.33f);
            list.Add(item7);

            GeneralProductInfo[] array = new GeneralProductInfo[7];
            list.CopyTo(array, 0);

            IEnumerator enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(array[0], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[1], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[2], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[3], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[4], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[5], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(array[6], enumerator.Current);
        }

        [TestMethod]
        public void InvalidateNode_EqualsNull()
        {
            GeneralProductInfo item1 = new GeneralProductInfo("Prekė_3", 10, 2.22f);
            GeneralProductInfo item2 = new GeneralProductInfo("Prekė_3", 20, 7.22f);
            LinkList<GeneralProductInfo>.Node<GeneralProductInfo> node2 = new LinkList<GeneralProductInfo>.Node<GeneralProductInfo>(item2, null);
            LinkList<GeneralProductInfo>.Node<GeneralProductInfo> node1 = new LinkList<GeneralProductInfo>.Node<GeneralProductInfo>(item1, node2);
            node1.Invalidate();
            node1.Data.Should().BeNull();
            node1.Link.Should().BeNull();
        }
    }
}
